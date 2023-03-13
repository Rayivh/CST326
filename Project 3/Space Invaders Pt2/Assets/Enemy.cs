using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AudioClip enemyShoot;
    public AudioClip enemyDeath;
    private EnemyManager gridScript;
    
    public int score;
    public float fireRateMin;
    public float fireRateMax;
    public static event Action<GameObject> OnDeath;
    void OnDestroy()
    {
        OnDeath?.Invoke(gameObject);
    }

    public void setAnimParams(string t, int sc, Sprite sp, string clipName)
    {
        tag = t;
        score = sc;
        GetComponent<SpriteRenderer>().sprite = sp;
        GetComponent<Animator>().Play(clipName);
    }
    private void FireProjectile()
    {
        GetComponent<AudioSource>().PlayOneShot(enemyShoot);
        float shotInterval = Random.Range(fireRateMin, fireRateMax);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.tag = "EnemyMissile";
        Invoke("FireProjectile", shotInterval);
    }

    public void DeathProcess()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<Animator>().Play("EnemyDeath");
        GetComponent<AudioSource>().PlayOneShot(enemyDeath);
        StartCoroutine("Die");
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
