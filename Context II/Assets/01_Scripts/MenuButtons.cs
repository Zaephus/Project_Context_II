using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject optionsMenu;
    Vector3 optionsStartPos = new Vector3(-125, 0, 0);
    Vector3 optionsEndPos = new Vector3(-725, 0, 0);
    float perc;
    float lerpTime = 0.02f;
    float currentLerpTime = 0f;
    public bool optionsActive = false;

    void FixedUpdate()
    {
        
    }

    public void OptionsToggle() {
        if (!optionsActive) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            perc = currentLerpTime / lerpTime;
            optionsMenu.transform.localPosition = Vector3.Lerp(optionsStartPos, optionsEndPos, perc);
            optionsActive = true;
        }
        else {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            perc = currentLerpTime / lerpTime;
            optionsMenu.transform.localPosition = Vector3.Lerp(optionsEndPos, optionsStartPos, perc);
            optionsActive = false;
        }
    }
}
