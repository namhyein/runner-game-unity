using UnityEngine;
using UnityEngine.Networking;

using System;
using System.Collections;
using System.Threading.Tasks;

using Apple.GameKit;
using Newtonsoft.Json;
using Unity.Services.Authentication;

using PlayerData;


public class UserManager
{
    private Player _user;
    private bool _isLoaded = false;
    public event Action<bool> OnLoaded;

    public bool IsLoaded
    {
        get { return _isLoaded; }
        private set
        {
            if (_isLoaded == value) return;
            _isLoaded = value;
            OnLoaded?.Invoke(value);
        }
    }

#if !UNITY_EDITOR && UNITY_IOS
    public async void Init()
    {
        try
        {
            GetLocalData();
            await GetAppleData();
            await Login();
            await SignInWithAppleGameCenterAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private async Task GetAppleData()
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            await GKLocalPlayer.Authenticate();
            var localPlayer = GKLocalPlayer.Local;

            _user.ID = localPlayer.GamePlayerId;
            _user.Nickname ??= localPlayer.DisplayName;
        }
    }

    private async Task SignInWithAppleGameCenterAsync()
    {
        try
        {
            var fetchItemsResponse = await GKLocalPlayer.Local.FetchItems();

            ulong timestamp = fetchItemsResponse.Timestamp;
            string publicKeyUrl = fetchItemsResponse.PublicKeyUrl;
            string salt = Convert.ToBase64String(fetchItemsResponse.GetSalt());
            string signature = Convert.ToBase64String(fetchItemsResponse.GetSignature());

            await AuthenticationService.Instance.SignInWithAppleGameCenterAsync(
                signature,
                GKLocalPlayer.Local.TeamPlayerId,
                publicKeyUrl,
                salt,
                timestamp
            );
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
#elif !UNITY_EDITOR && UNITY_AOS
    public async void Init()
    {
        GetLocalData();
        GetGoogleData();
        await Login();
    }

    private void GetGoogleData()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                _user._id = Social.localUser.id;
                _user.nickname ??= Social.localUser.userName;
            }
        });
    }
#else
    public async void Init()
    {
        GetLocalData();
        await Login();
    }
#endif

    private async Task Login()
    {
        string jsonBody = _user.Serialize();
        string endpoint = $"/users/{_user.ID}";

        using UnityWebRequest request = Managers.API.CreateUnityWebRequest("POST", endpoint, jsonBody);
        await Task.Yield();
        request.SendWebRequest();
        while (!request.isDone)
            await Task.Yield();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            _user.Update(response);
        }
        else Debug.LogError(request.error);
        IsLoaded = true;
    }

    private void GetLocalData()
    {
        if (PlayerPrefs.HasKey("user"))
            _user = JsonConvert.DeserializeObject<Player>(PlayerPrefs.GetString("user"));
        else
            _user = new Player()
            {
                ID = CreateUserId(),
                Nickname = "Guest"
            };
    }

    private string CreateUserId()
    {
        return Guid.NewGuid().ToString();
    }
}
