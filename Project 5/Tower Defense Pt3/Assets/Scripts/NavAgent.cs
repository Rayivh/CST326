using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject endpoint;
    void Start()
    {
        endpoint = GameObject.Find("EnemyEnd");
        GetComponent<NavMeshAgent>().SetDestination(endpoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        // if (Physics.Raycast(clickRay, out RaycastHit hitInfo, Mathf.Infinity))
        // {
        //     GetComponent<NavMeshAgent>().SetDestination(hitInfo.point);
        // }
        
        if (Vector3.Distance(transform.position, endpoint.transform.position)<= 0.2f)
        {
            EndPath();
        }
    }
    
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
