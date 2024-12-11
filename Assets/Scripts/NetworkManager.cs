using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    private string url = "http://172.20.10.2:12345/"; // Replace with your server's IP

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }


    // Method to fetch data from the Flask server
    public void FetchData(Action<string> onComplete)
    {
        StartCoroutine(GetRequest(onComplete));
    }

    private IEnumerator GetRequest(Action<string> onComplete)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                onComplete?.Invoke(null); // Pass null in case of an error
            }
            else
            {
                // Log the server's response
                string response = webRequest.downloadHandler.text;
                onComplete?.Invoke(response); // Pass the response back
            }
        }
    }
    // Method to extract value from JSON string
    public int ExtractValueFromJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("JSON data is null or empty.");
            return -1; // Return an error code or default value
        }

        try
        {
            // Parse the JSON string and extract the "value" field
            var jsonData = JsonUtility.FromJson<JsonData>(json);
            return jsonData.value;
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to parse JSON: " + ex.Message);
            return -1; // Return an error code or default value
        }
    }

    [Serializable]
    private class JsonData
    {
        public int value; // Match the "value" key in the JSON
    }
}
