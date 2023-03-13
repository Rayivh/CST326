using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Sprite playerSprite;
    public AudioClip playerShoot;
    public AudioClip playerDeath;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = playerSprite;
        GetComponent<Animator>().Play("Player Idle");
    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (transform.position.x > 15)
        {
            transform.position = new Vector3(15, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -15)
        {
            transform.position = new Vector3(-15, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate(new Vector3(horizontalAxis, 0f, 0f) * (15 * Time.deltaTime));
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().Play("Player Shoot");
            GetComponent<AudioSource>().PlayOneShot(playerShoot);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.tag = "PlayerMissile";
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>().LoadGameScene("Credits");
    }
    
    public void DeathProcess()
    {
        GetComponent<Animator>().Play("PlayerDeath");
        StartCoroutine("Die");
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
