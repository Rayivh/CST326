using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public GameObject rayCaster;
    public GameObject coinPrefab;
    private UIController uiController;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        uiController = rayCaster.GetComponent<UIController>();
        uiController.time = 365;

    }
    void Update()
    {
        Vector3 rayOrigin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(rayOrigin, Vector3.forward, out var hitInfo, 2))
            {
                GameObject target = hitInfo.collider.gameObject;
                switch (target.tag)
                {
                    case "Brick":
                        Destroy(target);
                        uiController.score += 100;
                        break;
                    
                    case "Question":
                        uiController.coins += 1;
                        Instantiate(coinPrefab, target.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}
