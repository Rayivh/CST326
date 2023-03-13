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

        var shapeModule = GetComponent<ParticleSystem>().shape;
        shapeModule.rotation = direction;
        GetComponent<ParticleSystem>().Play();
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
        switch (other.gameObject.layer)
        {
            case 7: // Enemy Layer
                Enemy eScript = other.gameObject.GetComponent<Enemy>();
                eScript.Invoke(nameof(eScript.DeathProcess), 0f);
                break;
            
            case 8: // Player Layer
                PlayerController pScript = other.gameObject.GetComponent<PlayerController>();
                pScript.Invoke(nameof(pScript.DeathProcess), 0f);
                break;
            
            case 9: // Bullet Layer
                Destroy(other.gameObject);
                break;
        }
        Destroy(gameObject);
    }
}
