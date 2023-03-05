using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraMovement : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;

    private Camera mainCamera;

    private float horizontal;
    private float vertical;

    private void Start() {
        mainCamera = GetComponent<Camera>();
    }

    private void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(horizontal, 0, vertical);
        velocity.Normalize();

        transform.position += velocity * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y < 0f && mainCamera.orthographicSize <= 20) { // zoom in
            mainCamera.orthographicSize++;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y > 0f && mainCamera.orthographicSize >= 2) {  // zoom out
            mainCamera.orthographicSize--;
        };
    }

}