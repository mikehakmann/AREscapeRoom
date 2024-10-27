using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // URL for the localhost server
    private string url = "http://127.0.0.1:5000/"; // replace with your endpoint

    // Method to initiate a GET request
    public void FetchData()
    {
        StartCoroutine(GetRequest());
    }

    // Method to initiate a POST request
    public void SendData(string dataToSend)
    {
        StartCoroutine(PostRequest(dataToSend));
    }

    // Coroutine to handle GET request
    IEnumerator GetRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Successfully received a response
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }

    // Coroutine to handle POST request
    IEnumerator PostRequest(string jsonData)
    {
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Successfully sent data and received a response
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
        }
    }
}
