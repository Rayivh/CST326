using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiveDemo : MonoBehaviour
{
    public GameObject package;
    public GameObject parachuteObject;
    public float parachuteHeight = 5f;
    public float parachuteDrag = 7.5f;
    public float landProximity = 1.1f;
    
    private float originalDrag;
    // Start is called before the first frame update
    void Start()
    {
        originalDrag = package.GetComponent<Rigidbody>().drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (!package)
        {
            return;
        }
        Vector3 rayOrigin = package.transform.position;
        Vector3 rayDirection = Vector3.down;

        if(Physics.Raycast(rayOrigin,rayDirection, out var hitInfo, parachuteHeight))
        {
            parachuteObject.SetActive(true);
            package.GetComponent<Rigidbody>().drag = parachuteDrag;
            Debug.DrawRay(package.transform.position, Vector3.down * parachuteHeight, Color.red, 0);
            if (hitInfo.distance < landProximity)
            {
                Destroy(package);
            }
        }
        else
        {
            parachuteObject.SetActive(false);
            package.GetComponent<Rigidbody>().drag = originalDrag;
            Debug.DrawRay(package.transform.position, Vector3.down * parachuteHeight, Color.blue, 0);
        }

  
    }
}
