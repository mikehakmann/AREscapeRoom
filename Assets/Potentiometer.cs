using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potentiometer : MonoBehaviour
{
    public float[] correctValues = new float[]{
        0.2f, 0.8f, 0.5f
    };
    public float allowedErrorMargin = 0.05f;
    private float currPotentiometerValue = 0;
    private float lastPotentiometerValue = 0;
    private Canvas canvas;
    private Image backdrop;
    private Image currentValue;
    private Image targetValue;
    private float maxAngle = 360;
    private int currValueIndex = 0;
    private float unlockDuration = 0.3f;

    void Awake()
    {
        GetComponents();
        StartCoroutine(LockPicking());
    }

    private void GetComponents()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        backdrop = GameObject.Find("Backdrop").GetComponent<Image>();
        currentValue = GameObject.Find("CurrentValue").GetComponent<Image>();
        targetValue = GameObject.Find("TargetValue").GetComponent<Image>();
    }

    void Update()
    {
        currPotentiometerValue = (float)NetworkManager.singleton.lastResponse.data.potentiometer / 4095;

        if (currValueIndex < correctValues.Length)
        {
            ShowPotentiometer();

            currentValue.transform.localRotation = Quaternion.Euler(0f, 0f, currPotentiometerValue * maxAngle);

            targetValue.enabled = true;
            targetValue.fillAmount = allowedErrorMargin * 2;
            targetValue.transform.localRotation = Quaternion.Euler(0f, 0f, correctValues[currValueIndex] * maxAngle);
        }
        else
        {
            HidePotentiometer();
        }
    }

    public IEnumerator LockPicking()
    {
        while (currValueIndex < correctValues.Length)
        {
            if (targetValue == null || !targetValue.enabled)   // Do not do logic while there are no visuals
                continue;

            yield return null;
            if (Mathf.Abs(currPotentiometerValue - correctValues[currValueIndex]) <= allowedErrorMargin)
            {
                lastPotentiometerValue = currPotentiometerValue;

                yield return new WaitForSeconds(unlockDuration);
                if (Mathf.Abs(lastPotentiometerValue - correctValues[currValueIndex]) <= allowedErrorMargin && Mathf.Abs(currPotentiometerValue - correctValues[currValueIndex]) <= allowedErrorMargin)
                {
                    currValueIndex++;
                }
            }
        }

        // TODO - Success indicator
    }

    public void ShowPotentiometer()
    {
        if (canvas == null)
        {
            GetComponents();
            Debug.Log("No canvas.");
        }

        canvas.enabled = true;
        backdrop.enabled = true;
        targetValue.enabled = true;
        currentValue.enabled = true;
    }

    public void HidePotentiometer()
    {
        if (canvas == null)
        {
            GetComponents();
            Debug.Log("No canvas.");
        }

        canvas.enabled = false;
        backdrop.enabled = false;
        targetValue.enabled = false;
        currentValue.enabled = false;
    }
}
