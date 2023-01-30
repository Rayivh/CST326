using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    public float localRotationDegrees = 45;
    public float orbitRotationDegrees = 45;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (target.transform.position + offset);

        //Rotate around myself
        transform.Rotate(new Vector3(0f, localRotationDegrees * Time.deltaTime, 0), Space.World);

        //Orbit around my target
        transform.RotateAround(target.transform.position, Vector3.up, orbitRotationDegrees * Time.deltaTime);
        
        offset = transform.position - target.transform.position;
    }
}
