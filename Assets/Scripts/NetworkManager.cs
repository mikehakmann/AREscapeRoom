using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public JsonResponse lastResponse = new JsonResponse();
    public static NetworkManager singleton;
    private string url = "http://192.168.220.41:12345"; // Replace with your Flask server's IP

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this); // Ensure only one instance exists
        }
        else
        {
            singleton = this;
        }
    }

    void Start()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                string jsonResponse = request.downloadHandler.text;

                try
                {
                    // Deserialize the JSON response into an object
                    lastResponse = JsonUtility.FromJson<JsonResponse>(jsonResponse);
                    Debug.Log($"JSON Response: {jsonResponse}");

                    // Access the data
                    if (lastResponse != null && lastResponse.data != null)
                    {
                        Debug.Log($"Fetched values from server: Potentiometer={lastResponse.data.potentiometer}, Switch={lastResponse.data.switchValue}");
                    }
                    else
                    {
                        Debug.LogError("Error: Data not found in response.");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error parsing JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"GET Error: {request.error}");
            }

            yield return null;
        }
    }

    // Coroutine to handle POST requests (optional, left for context)
    public IEnumerator PostData(int switchValue, int potentiometerValue)
    {
        // Create a JSON payload
        string jsonPayload = JsonUtility.ToJson(new JsonData { potentiometer = potentiometerValue, switchValue = switchValue });

        // Configure the request
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"POST Response: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"POST Error: {request.error}");
        }
    }

    [Serializable]
    public class JsonData
    {
        public int potentiometer; // Matches "potentiometer" in the JSON
        public int switchValue;   // Matches "switch" in the JSON
    }

    [Serializable]
    public class JsonResponse
    {
        public string message;   // Matches "message" in the JSON
        public JsonData data;    // Matches "data" in the JSON
    }
}