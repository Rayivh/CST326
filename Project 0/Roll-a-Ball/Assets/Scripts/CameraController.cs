using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Keep us at the last known relative position
        transform.position = (player.transform.position + offset);
        if (Input.GetKey("q"))
        {
            transform.RotateAround(player.transform.position, Vector3.up, 45 * Time.deltaTime);
        }
        if (Input.GetKey("e"))
        {
            transform.RotateAround(player.transform.position, Vector3.up, -45 * Time.deltaTime);
        }


        // Update new offset after rotation mucks w/ the coordinates
        offset = transform.position - player.transform.position;
    }
}
