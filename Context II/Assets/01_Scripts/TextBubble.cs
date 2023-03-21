using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubble : MonoBehaviour {

    public static System.Action<bool> ToggledVisibility;

    private void Start() {
        ToggledVisibility += ToggleVisibility;
    }

    private void ToggleVisibility(bool _value) {
        gameObject.SetActive(_value);
    }

    private void OnDestroy() {
        ToggledVisibility -= ToggleVisibility;
    }

}