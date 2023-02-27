using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Directly invoke method (instead of going through State update)
            //other.gameObject.GetComponent<PlayerController>().Invoke("DeathAnimation", 0f);

            other.gameObject.GetComponent<PlayerController>().state = "Dead";
            Debug.Log("You've Been Goomba'd!");
        }
    }
}
