using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {
    public GameObject optionsMenu;
    Vector2 optionsStartPos = new Vector2(-725, 0);
    Vector2 optionsEndPos = new Vector2(-125, 0);
    float perc;
    float lerpTime = 0.02f;
    float currentLerpTime = 0f;
    public bool optionsActive = false;

    void Start() {
    }
    void FixedUpdate() {

    }
    IEnumerator LerpPosition(Vector2 startPosition, Vector2 targetPosition, float duration) {
        float time = 0;
        while (time < duration) {
            optionsMenu.transform.localPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        optionsMenu.transform.localPosition = targetPosition;
    }
    public void OptionsToggle() {

        if (!optionsActive) {
            StartCoroutine(LerpPosition(optionsStartPos, optionsEndPos, 0.7f));
            optionsActive = true;
        }
        else {
            StartCoroutine(LerpPosition(optionsEndPos, optionsStartPos, 0.7f));
            optionsActive = false;
        }
    }
}
