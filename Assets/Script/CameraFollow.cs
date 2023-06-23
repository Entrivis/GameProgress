using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    private float currentPosition;
    public Transform target;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
        // print(startPosition);
    }
    private void LateUpdate()
    {
        // print(startPosition);
        currentPosition = Mathf.Max(target.position.x, transform.position.x);
        Vector3 CameraPosition = new Vector3(currentPosition, startPosition.y, startPosition.z);
        transform.position = CameraPosition;
    }
}