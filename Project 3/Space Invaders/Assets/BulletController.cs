using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float projectileDropOff = 9f;
    private Vector3 direction;
    void Start()
    {
        switch (gameObject.tag)
        {
            case "EnemyMissile":
                gameObject.GetComponent<Collider>().excludeLayers =LayerMask.GetMask("Enemy");
                gameObject.GetComponent<Collider>().includeLayers =LayerMask.GetMask("Player", "Bullet");
                direction = Vector3.down;
                break;
            
            case "PlayerMissile":
                gameObject.GetComponent<Collider>().excludeLayers =LayerMask.GetMask("Player");
                gameObject.GetComponent<Collider>().includeLayers =LayerMask.GetMask("Enemy", "Bullet");
                direction = Vector3.up;
                break;
        }
    }
    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));

        if (Math.Abs(transform.position.y) > projectileDropOff)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // I love a good murder-suicide
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
