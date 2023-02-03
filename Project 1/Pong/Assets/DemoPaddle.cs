using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPaddle : MonoBehaviour
{
    public float unitsPerSecond = 6f;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        Vector3 force = Vector3.right * horizontalValue;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"we hit {collision.gameObject.name}");

        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxX = bounds.max.x;
        float maxY = bounds.max.y;

        Debug.Log($"maxX = {maxX}, maxY = {maxY}");
        Debug.Log($"x pos of ball is = {collision.transform.position.x}");
        
        Quaternion rotation = Quaternion.Euler(60f, 0f, -90f);
        Vector3 bounceDirection = rotation * Vector3.up;
        
        Rigidbody rb = collision.rigidbody;
        rb.AddForce(bounceDirection * 1000f, ForceMode.Force);
    }

}

