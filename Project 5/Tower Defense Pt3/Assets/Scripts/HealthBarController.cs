using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
        GetComponent<Canvas>().worldCamera = camera;
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}
