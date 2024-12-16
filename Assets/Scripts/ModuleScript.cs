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
        networkManager = NetworkManager.singleton;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager instance is null. Ensure NetworkManager is in the scene and properly initialized.");
            return;
        }

        StartCoroutine(CheckDataEveryHalfSecond());
    }

    private IEnumerator CheckDataEveryHalfSecond()
    {
        while (true)
        {
            StartCoroutine(networkManager.GetData());
            yield return new WaitForSeconds(0.5f);
        }
    }
}