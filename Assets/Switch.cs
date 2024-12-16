using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private SpriteRenderer switchIndicator;

    void Awake()
    {
        GetImageComponent();
    }

    private void GetImageComponent()
    {
        switchIndicator = GameObject.Find("SwitchImage").GetComponent<SpriteRenderer>();
    }

    public void ShowSwitch()
    {
        if (switchIndicator != null)
        {
            switchIndicator.enabled = true;
        }
        else
        {
            Debug.Log("No switch.");
            GetImageComponent();
        }
    }

    public void HideSwitch()
    {
        if (switchIndicator != null)
        {
            switchIndicator.enabled = false;
        }
        else
        {
            Debug.Log("No switch.");
            GetImageComponent();
        }
    }
}
