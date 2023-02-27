using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCaster : MonoBehaviour
{
    public GameObject rayCaster;
    public GameObject coinPrefab;
    Transform parentTransform;
    Collider parentCollider;
    
    UIController uiController;

    bool hit;
    float _cooldownElapsed;
    bool _click;
    
    //Camera cam;
    void Start()
    {
        //cam = Camera.main;
        uiController = rayCaster.GetComponent<UIController>();
        uiController.time = 100;
        parentTransform = GetComponentInParent<Transform>();
        parentCollider = GetComponentInParent<Collider>();
        

    }
    void Update()
    {
        //Vector3 rayOrigin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1));
        Vector3 rayOrigin = new Vector3(parentTransform.position.x, parentCollider.bounds.max.y, parentTransform.position.z);

        Color lineColor = hit ? Color.green : Color.red;
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.up * 0.03f,lineColor, 0f, false);

        hit = Physics.Raycast(rayOrigin, Vector3.up, out var hitInfo, 0.03f);
        
        if(hit)
        {
            GameObject target = hitInfo.collider.gameObject;
            switch (target.tag)
            {
                case "Brick":
                    Destroy(target);
                    uiController.score += 100;
                    break;
                
                case "Question":
                    if (_cooldownElapsed > 0.5f)
                    {
                        _cooldownElapsed = 0;
                        
                        uiController.score += 100;
                        uiController.coins += 1;
                        Instantiate(coinPrefab, target.transform.position, Quaternion.identity);
                        
                    }
                    break;
            }
        }
        else
        {
            _cooldownElapsed += Time.deltaTime;
        }
    }
}
