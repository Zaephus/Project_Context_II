using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

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
    }

}