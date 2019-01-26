using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        playerTransform.position = cameraTransform.position;
        rb.freezeRotation = true;
    }
}
