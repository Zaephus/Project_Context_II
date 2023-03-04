using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Camera m_Camera;

    [SerializeField]
    private float moveSpeed;

    private float horizontal;
    private float vertical;

    private void Start() {}

    private void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        Vector3 velocity = new Vector3(horizontal, 0, vertical);
        velocity.Normalize();

        transform.position += velocity * moveSpeed * Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && m_Camera.orthographicSize <= 20) // zoom in
        {
            m_Camera.orthographicSize++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && m_Camera.orthographicSize >= 2)  // zoom out
        {
            m_Camera.orthographicSize--;
        };
    }

}