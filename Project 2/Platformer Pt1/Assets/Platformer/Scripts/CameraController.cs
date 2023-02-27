using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    private Camera cam;
    public GameObject character;
    private Rigidbody _rb;

    void Start()
    {
        _rb = character.GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        Vector3 screenPos = cam.WorldToScreenPoint(character.transform.position);

        if (screenPos.x > Screen.width/2)
        {
            transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
        }

        if (screenPos.x < 10)
        { 
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            character.transform.position = cam.ScreenToWorldPoint(new Vector3(11, screenPos.y, screenPos.z));
        }
    }
    //if mario is at or beyond half the screen, camera will proceed with him
    //if mario is not, camera will not move.
}
