using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    private EnemyManager gridScript;
    
    private int score;
    private float fireRateMin;
    private float fireRateMax;
    public static event Action<GameObject> OnDeath;
    void OnDestroy()
    {
        OnDeath?.Invoke(gameObject);
    }
    
    void Start()
    {
        gridScript = GameObject.Find("EnemyGrid").GetComponent<EnemyManager>();
        switch (gameObject.tag)
        {
            case "Shooter":
                score = 30;
                fireRateMin = gridScript.fireRateMin;
                fireRateMax = gridScript.fireRateMax;
                Invoke("FireProjectile", Random.Range(fireRateMin, fireRateMax));
                break;
            
            case "Crab":
                score = 20;
                break;
            
            case "Jelly":
                score = 10;
                break;
            
            case "UFO":
                score = Random.Range(50,300);
                break;
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("UFO"))
        {
            transform.Translate(Vector3.right * (7.5f * Time.deltaTime));
            if (transform.position.x > gridScript.maxDistance)
            {
                score = 0;
                Destroy(gameObject);
            }
        }
    }

    private void FireProjectile()
    {
        float shotInterval = Random.Range(fireRateMin, fireRateMax);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.tag = "EnemyMissile";
        Invoke("FireProjectile", shotInterval);
    }

    public int GetScore()
    {
        return score;
    }
}
