using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Transform jakisTamTransform;

    void Update() {
        jakisTamTransform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
