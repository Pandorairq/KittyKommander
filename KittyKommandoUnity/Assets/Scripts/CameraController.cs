using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        cam.transform.position = transform.position + Vector3.back * 10;
    }
}
