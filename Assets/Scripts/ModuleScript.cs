using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModuleScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private NetworkManager networkManager;

    // Start is called before the first frame update
    private void Start()
    {
        networkManager = NetworkManager.instance;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager instance is null. Ensure NetworkManager is in the scene and properly initialized.");
            return;
        }

        StartCoroutine(CheckDataEveryHalfSecond());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDataFetched(string data)
    {
        Debug.Log(data);
        if (networkManager.ExtractValueFromJson(data) == 1)
        {
            text.text = "ookaaay se det her " +data;
        }
        else
        {
            text.text = "arrrgh det for dårligt du " + data;
        }
    }

    private IEnumerator CheckDataEveryHalfSecond()
    {
        while (true)
        {
            networkManager.FetchData(OnDataFetched);
            yield return new WaitForSeconds(0.5f);
        }
    }
}