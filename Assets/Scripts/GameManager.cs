using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Intro,
    WeightRiddle,
    HandsensorRiddle
}

public class GameManager : MonoBehaviour
{
    private NetworkManager networkManager;
    public GameState gameState = GameState.Intro;

    public GameObject IntroObjects;
    public GameObject WeightRiddle;
    public GameObject HandsensorRiddle;

    // Start is called before the first frame update
    private void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    if (gameState == GameState.Intro)
    //    {
    //        // Check if the player has clicked on the start button on the UI
    //        // If so, change the gameState to WeightRiddle
    //        if (IntroObjects.activeSelf == false)
    //        {
    //            IntroObjects.SetActive(true);
    //            networkManager.SendData("{\"Intro\": true}");
    //        }
    //    }
    //    else if (gameState == GameState.WeightRiddle)
    //    {
    //        // Check if the player has solved the weight riddle
    //        // If so, change the gameState to HandsensorRiddle

    //        if (WeightRiddle.activeSelf == false)
    //        {
    //            IntroObjects.SetActive(false);
    //            WeightRiddle.SetActive(true);
    //            networkManager.SendData("{\"WeightRiddle\": true}");
    //        }
    //    }
    //    else if (gameState == GameState.HandsensorRiddle)
    //    {
    //        // Check if the player has solved the handsensor riddle
    //        // If so, change the gameState to Intro
    //        if (HandsensorRiddle.activeSelf == false)
    //        {
    //            WeightRiddle.SetActive(false);
    //            HandsensorRiddle.SetActive(true);
    //            networkManager.SendData("{\"HandsensorRiddle\": true}");
    //        }
    //    }
    //}

    //public void EndTask(int taskNumber)
    //{
    //    string objectName = "task" + taskNumber;
    //    string jsonData = "{\"" + objectName + "\": {\"number\": " + taskNumber + ", \"finished\": true}}";
    //    networkManager.SendData(jsonData);
    //}

    //private void OnApplicationQuit()
    //{
    //    networkManager.SendData("{\"quit\": true}");
    //}
}