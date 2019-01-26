using System.Collections.Generic;
using Code.Enemy;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform jakisTamTransform;

    private void FixedUpdate()
    {
        jakisTamTransform.position = new Vector3(transform.position.z , transform.position.y, -10f);
    }
}