using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTask(int taskNumber)
    {
        string objectName = "task" + taskNumber;
        string jsonData = "{\"" + objectName + "\": {\"number\": " + taskNumber + ", \"finished\": true}}";
        networkManager.SendData(jsonData);
    }

    private void OnApplicationQuit()
    {
        networkManager.SendData("{\"quit\": true}");
    }
    


}
