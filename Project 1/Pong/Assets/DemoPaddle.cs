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
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     // Triple Ternary Terror!
    //     // Explained:  First Ternary: accounts for dead-center collision
    //     //   (Nested) Second Ternary: flips angle degree variance                                  depending on player/side
    //     //             Third Ternary: flips euler axis (referenced by angle variation multiplier)  depending on player/side
    //
    //     if (other.gameObject.name is "Top" or "Bottom")
    //     {
    //         _rb.velocity = Vector3.zero;
    //     }
    //     
    //     if (other.gameObject.tag is "Ball")
    //     {
    //         Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
    //         float heightDif = transform.position.y - other.gameObject.transform.position.y;
    //         Vector3 bounceDirection = Quaternion.Euler(0f, 0f, heightDif == 0 ? 0f : heightDif * (player1 ? -45f : 45f)) * (player1 ? Vector3.right : Vector3.left);
    //         Debug.Log($"Bouncing by {(heightDif * (player1 ? -45f : 45f)).ToString("F2")} Degrees");
    //
    //         rb.velocity = Vector3.zero;
    //         if (incrementForce)
    //         {
    //             force += 1000f;
    //         }
    //         rb.AddForce(bounceDirection * force, ForceMode.Force);
    //     }
    // }
}

