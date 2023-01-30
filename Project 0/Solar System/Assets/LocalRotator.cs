using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotator : MonoBehaviour
{
    public float localRotationDegrees = 45;
    public float orbitRotationDegrees = 45;

    // Update is called once per frame
    void Update()
    {
        Transform t = GetComponent<Transform>();
        
        //Rotate around myself
        t.Rotate(new Vector3(0f, localRotationDegrees * Time.deltaTime, 0));

        //Orbit around my parent's position
        if (gameObject.transform.parent)
        {
            t.RotateAround(gameObject.transform.parent.transform.position, Vector3.up, orbitRotationDegrees * Time.deltaTime);
        }
        
    }
}
