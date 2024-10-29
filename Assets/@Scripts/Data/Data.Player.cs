using System;
using System.Collections.Generic;
using AmplitudeNS.MiniJSON;
using Newtonsoft.Json;
using UnityEngine;

namespace PlayerData
{
  [Serializable]
  public class Player
  {
    private string _id;
    private string nickname;
    private int dia = 0;
    private int coin = 0;
    private int potion = 0;
    private bool push = true;
    private bool removeAd = false;
    private bool isInternal = false;
    private bool infiniteHeart = false;

    public event Action<string> OnIDChanged;
    public event Action<string> OnNicknameChanged;
    public event Action<int> OnDiaChanged;
    public event Action<int> OnCoinChanged;
    public event Action<int> OnPotionChanged;
    public event Action<bool> OnPushChanged;
    public event Action<bool> OnRemoveAdChanged;
    public event Action<bool> OnIsInternalChanged;
    public event Action<bool> OnInfiniteHeartChanged;

    public string ID
    {
      get { return _id; }
      set
      {
        if (_id == value) return;
        _id = value;
        OnIDChanged?.Invoke(_id);
      }
    }

    public string Nickname
    {
      get { return nickname; }
      set
      {
        if (nickname == value) return;
        nickname = value;
        OnNicknameChanged?.Invoke(nickname);
      }
    }

    public int Dia
    {
      get { return dia; }
      set
      {
        if (dia == value) return;
        dia = value;
        OnDiaChanged?.Invoke(dia);
      }
    }

    public int Coin
    {
      get { return coin; }
      set
      {
        if (coin == value) return;
        coin = value;
        OnCoinChanged?.Invoke(coin);
      }
    }

    public int Potion
    {
      get { return potion; }
      set
      {
        if (potion == value) return;
        potion = value;
        OnPotionChanged?.Invoke(potion);
      }
    }

    public bool Push
    {
      get { return push; }
      set
      {
        if (push == value) return;
        push = value;
        OnPushChanged?.Invoke(push);
      }
    }

    public bool RemoveAd
    {
      get { return removeAd; }
      set
      {
        if (removeAd == value) return;
        removeAd = value;
        OnRemoveAdChanged?.Invoke(removeAd);
      }
    }


    public bool IsInternal
    {
      get { return isInternal; }
      set
      {
        if (isInternal == value) return;
        isInternal = value;
        OnIsInternalChanged?.Invoke(isInternal);
      }
    }

    public bool InfiniteHeart
    {
      get { return infiniteHeart; }
      set
      {
        if (infiniteHeart == value) return;
        infiniteHeart = value;
        OnInfiniteHeartChanged?.Invoke(infiniteHeart);
      }
    }

    public string version = Application.version;

    public string Serialize()
    {
      return JsonConvert.SerializeObject(
        new Dictionary<string, object>
        {
          { "id", _id },
          { "nickname", nickname },
          { "dia", dia },
          { "coin", coin },
          { "potion", potion },
          { "push", push },
        }
      );
    }

    public void Update(string json)
    {
      Player player = JsonConvert.DeserializeObject<Player>(json);

      _id = player._id;
      nickname = player.nickname;

      dia = Mathf.Max(dia, player.dia);
      coin = Mathf.Max(coin, player.coin);
      potion = Mathf.Max(potion, player.potion);

      removeAd = player.removeAd;
      isInternal = player.isInternal;
      infiniteHeart = player.infiniteHeart;
    }
  }
}
