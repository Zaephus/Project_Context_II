using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public static System.Action<bool> CursorLocked;
    public static System.Action CameraReset;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float zoomSpeed;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float minTransitionValue;
    [SerializeField]
    private float maxTransitionValue;
    [SerializeField]
    private float transitionSpeed;

    private Vector3 mouseDelta;
    private float scrollDelta;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startZoom;

    private bool isInStartTransition = false;

    public void Start() {
        StartCoroutine(StartTransition());
        CameraReset += ResetCamera;
    }

    private void Update() {
        if(!isInStartTransition) {
            OnUpdate();
        }
    }

    public void OnUpdate() {

        mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        mouseDelta.Normalize();

        scrollDelta = Input.mouseScrollDelta.y;

        if(Input.GetMouseButton(2)) {
            HideAndLockCursor();
            Vector3 moveDir = mouseDelta.x * transform.right + mouseDelta.z * transform.forward;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(2)) {
            ShowAndUnlockCursor();
        }

        if(Input.GetKey(KeyCode.LeftAlt)) {
            HideAndLockCursor();
            if(Input.GetMouseButton(0)) {
                transform.eulerAngles -= new Vector3(0, mouseDelta.x, 0) * rotateSpeed * Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftAlt)) {
            ShowAndUnlockCursor();
        }

        if(scrollDelta < 0.0f && mainCamera.orthographicSize <= 20) {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
        }
        if(scrollDelta > 0.0f && mainCamera.orthographicSize >= 2) {
            mainCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
        }

    }

    private IEnumerator StartTransition() {

        isInStartTransition = true;

        mainCamera.orthographicSize = minTransitionValue;

        float startSpeed = transitionSpeed;
        float endSpeed = transitionSpeed * 0.4f;

        float speed = 0;

        float completion = 0.0f;

        while(mainCamera.orthographicSize < maxTransitionValue) {

            mainCamera.orthographicSize += speed * Time.deltaTime;

            speed = Mathf.Lerp(startSpeed, endSpeed, completion);

            completion += (speed * Time.deltaTime) / (maxTransitionValue - minTransitionValue);
            Debug.Log(completion);
            yield return new WaitForEndOfFrame();

        }
        
        startPosition = transform.position;
        startRotation = transform.rotation;
        startZoom = mainCamera.orthographicSize;

        isInStartTransition = false;

    }

    private void ResetCamera() {
        transform.position = startPosition;
        transform.rotation = startRotation;
        mainCamera.orthographicSize = startZoom;
    }

    private void ShowAndUnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CursorLocked?.Invoke(false);
    }

    private void HideAndLockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CursorLocked?.Invoke(true);
    }

}