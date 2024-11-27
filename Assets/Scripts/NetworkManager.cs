using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string url = "http://172.20.10.2:12345/"; // Replace with your server's IP

    // Method to fetch data from the Flask server
    public void FetchData()
    {
        StartCoroutine(GetRequest());
    }

    private IEnumerator GetRequest()
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
                // Log the server's response
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
