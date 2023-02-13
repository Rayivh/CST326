using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class DemoCarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public Canvas hoverCanvas;
    public AudioClip hornClip;
    public float amplitude = 100f;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 carForward = transform.forward;
        // float currentoffset = Mathf.Sin(Time.realtimeSinceStartup) * amplitude;
        //
        // transform.position = startPosition + carForward * currentoffset;
        //
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     GetComponent<AudioSource>().PlayOneShot(hornClip);
        // }

        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 fromCarToCamera = mainCamera.transform.position - hoverCanvas.transform.position;
        fromCarToCamera.Normalize();
        
        Quaternion lookAtRotation = Quaternion.LookRotation(fromCarToCamera);
        lookAtRotation = Quaternion.Euler(0, lookAtRotation.eulerAngles.y, 0);
        hoverCanvas.transform.rotation = lookAtRotation;
        
        //hoverCanvas.transform.LookAt(cameraPos);
    }
}
