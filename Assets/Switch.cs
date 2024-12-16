using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private SpriteRenderer switchIndicator;
    [SerializeField] private Material onMetarial;
    [SerializeField] private Material offMaterial;
    private List<MeshRenderer> LEDs;
    private bool IsOn1 = false, IsOn2 = false, IsOn3 = false, IsOn4 = false, IsOn5 = false;
    [SerializeField] private GameObject fireworks;

    private void Awake()
    {
        LEDs = new List<MeshRenderer>();
        GetImageComponent();
    }

    private void Start()
    {
        TurnSwitchOnOff(1); 
        TurnSwitchOnOff(2); 
        TurnSwitchOnOff(3); 
        TurnSwitchOnOff(4); 
        TurnSwitchOnOff(5); 

        fireworks = GameObject.Find("Fireworks2");
        fireworks.SetActive(false);
    }

    private void GetImageComponent()
    {
        // Initialize the LEDs list to prevent null references
        if (LEDs == null)
            LEDs = new List<MeshRenderer>();
        else
            LEDs.Clear(); // Clear existing list to avoid duplicates

        // Find all GameObjects with the "LED" tag
        GameObject[] tempLeds = GameObject.FindGameObjectsWithTag("LED");

        foreach (GameObject ledParent in tempLeds)
        {
            // Locate the 'LED' child within each 'LED#' parent
            Transform led = ledParent.transform.Find("LED");
            if (led != null)
            {
                // Find Feet and Lamp under the 'LED' child
                Transform feet = led.Find("Feet");
                Transform lamp = led.Find("Lamp");

                // Enable MeshRenderer for Feet
                if (feet != null && feet.GetComponent<MeshRenderer>() != null)
                    feet.GetComponent<MeshRenderer>().enabled = true;

                // Enable MeshRenderer for Lamp and add to the LEDs list
                if (lamp != null && lamp.GetComponent<MeshRenderer>() != null)
                {
                    lamp.GetComponent<MeshRenderer>().enabled = true;
                    LEDs.Add(lamp.GetComponent<MeshRenderer>());
                }
                else
                {
                    Debug.Log("No lamp found.");
                }
            }
        }


        // Find and assign the SwitchImage SpriteRenderer component
        switchIndicator = GameObject.Find("SwitchImage").GetComponent<SpriteRenderer>();
    }

    public bool isOn = false;

    [ContextMenu("SwitchOn")]
    public void SwitchOnTEST()
    {
     TurnSwitchOnOff(2);
     TurnSwitchOnOff(4);
     TurnSwitchOnOff(5);

    }
    public void TurnSwitchOnOff(int index)
    {
        // Ensure the index is within range
        if (index < 1 || index > LEDs.Count)
        {
            Debug.LogError("Invalid index for TurnSwitchOn. Index must be between 1 and " + LEDs.Count);
            return;
        }

        // Fetch the appropriate LED MeshRenderer
        MeshRenderer led = LEDs[index - 1]; // Index - 1 because the list starts at 0

        switch (index)
        {
            case 1:
                IsOn1 = !IsOn1;
                led.material = IsOn1 ? onMetarial : offMaterial;
                break;
            case 2:
                IsOn2 = !IsOn2;
                led.material = IsOn2 ? onMetarial : offMaterial;
                break;
            case 3:
                IsOn3 = !IsOn3;
                led.material = IsOn3 ? onMetarial : offMaterial;
                break;
            case 4:
                IsOn4 = !IsOn4;
                led.material = IsOn4 ? onMetarial : offMaterial;
                break;
            case 5:
                IsOn5 = !IsOn5;
                led.material = IsOn5 ? onMetarial : offMaterial;
                break;
            default:
                Debug.LogError("Invalid switch index.");
                break;
        }


        //check if the right switches are on
        if (IsOn2 && IsOn4 && IsOn5)
        {
            fireworks.SetActive(true);
            Debug.Log("All the right switches are on");
        }


    }


    private void EndModule()
    {
    }


    public void ShowSwitch()
    {
        if (switchIndicator != null)
        {
            switchIndicator.enabled = true;
            GetImageComponent();
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