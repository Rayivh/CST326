using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * new Vector3(Input.GetAxis("Horizontal") , 0f, 0f));
    }
}
