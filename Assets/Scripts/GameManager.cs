using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private NetworkManager networkManager;
    private void Start()
    {
        networkManager = GameObject.Find("GameManager").GetComponent<NetworkManager>();
    }
}