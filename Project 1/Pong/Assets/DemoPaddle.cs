using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class DemoPaddle : MonoBehaviour
{
    public bool player1 = true;
    public float unitsPerSecond = 6f;

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float verticalValue;
        verticalValue = Input.GetAxis(player1 ? "Player1" : "Player2");
        
        Vector3 force = Vector3.up * (verticalValue * unitsPerSecond);
        _rb.AddForce(force, ForceMode.Force);
    }
}

