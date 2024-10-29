using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using System.Net.Http;


public class APIManager
{
  private static readonly HttpClient client = new();

  private readonly string API_HOST = "http://localhost:8000/api";
  private readonly string API_VERSION = "v1.1";

  public bool isInitialized = false;

  public UnityWebRequest CreateUnityWebRequest(string method, string endpoint, string jsonBody = null)
  {
    UnityWebRequest request = new($"{API_HOST}/{API_VERSION}{endpoint}", method)
    {
      downloadHandler = new DownloadHandlerBuffer()
    };

    if (!string.IsNullOrEmpty(jsonBody))
    {
      byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
      request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    }

    request.SetRequestHeader("Content-Type", "application/json");
    return request;
  }


  public async Task<string> CreateHttpRequest(string method, string endpoint, string jsonBody = null)
  {
    string url = $"{API_HOST}/{API_VERSION}{endpoint}";
    HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), url);
    if (!string.IsNullOrEmpty(jsonBody))
    {
      request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
    }
    try
    {
      HttpResponseMessage response = await client.SendAsync(request);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();
      return responseBody;
    }
    catch (HttpRequestException e)
    {
      Debug.Log("\nException Caught!");
      Debug.Log("Message :{0} " + e.Message.ToString());
      return null;
    }
  }
}