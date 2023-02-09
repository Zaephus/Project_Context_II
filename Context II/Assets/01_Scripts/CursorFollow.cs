using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    public Canvas parentCanvas;
    public GameObject cursorClick;

    public void Start() {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);
    }

    public void Update() {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        transform.position = parentCanvas.transform.TransformPoint(movePos);

        if (Input.GetMouseButtonDown(0)) {
            cursorClick.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0)) {
            cursorClick.SetActive(false);
        }
    }
}
