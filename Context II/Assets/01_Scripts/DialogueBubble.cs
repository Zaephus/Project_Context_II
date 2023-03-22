using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBubble : MonoBehaviour {

    public static System.Action<bool> ToggledVisibility;

    [HideInInspector]
    public Sprite sprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer.sprite = sprite;
        ToggledVisibility += ToggleVisibility;
    }

    private void ToggleVisibility(bool _value) {
        gameObject.SetActive(_value);
    }

    private void OnDestroy() {
        ToggledVisibility -= ToggleVisibility;
    }

}