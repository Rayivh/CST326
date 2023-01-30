using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;


    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (target.transform.position + offset);
        
        offset = transform.position - target.transform.position;

        //Not great, could be cleaner. Try uncouple from parent/child so we dont inherit local rotation
        transform.LookAt(target.transform);
    }
}
