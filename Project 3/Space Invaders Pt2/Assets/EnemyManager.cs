using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour {
   public List<GameObject> enemies;
   public GameObject enemyPrefab;
   
   public RuntimeAnimatorController animationController;
   public Sprite crabSprite;
   public Sprite squidSprite;
   public Sprite octopusSprite;
   public Sprite ufoSprite;

   public int maxDistance;
   public float gridWidth, gridHeight;
   public float speed = 5f;
   public float deathIncrement = 0.75f;
   public float fireRateMin = 5.0f;
   public float fireRateMax = 15.0f;
   private Vector3 direction;
   
   void Start()
   {
      Enemy.OnDeath += CleanCorpse;
      
      transform.Translate(-gridWidth/2, gridHeight/2, 0);
      direction = Vector3.right;


      //init grid
      for (int i = 0; i < gridHeight; i++)
      {
         for (int j = 0; j < gridWidth; j++)
         {
            GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(i, j, 0) * 1.5f, Quaternion.identity, transform);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemy.GetComponent<Animator>().runtimeAnimatorController = animationController;
            
            
            if (j == (int) gridWidth - 1)
            {
               enemyScript.setAnimParams("Shooter", 30, squidSprite, "Squid Idle");
               enemyScript.fireRateMax = fireRateMax;
               enemyScript.fireRateMin = fireRateMin;
            }
            else if (j == (int) gridWidth - 2)
            {
               enemyScript.setAnimParams("Crab", 20, crabSprite, "Crab Idle");
            }
            else
            {
               enemyScript.setAnimParams("Jelly", 10, octopusSprite, "Octo Idle");
            }
            enemies.Add(enemy);
         }
      }
   }

   void CleanCorpse(GameObject enemy)
   {
      if(!enemy.CompareTag("UFO"))
      {
         speed+=deathIncrement;
      }
      enemies.Remove(enemy);
   }
   
   void Update() {
      if (SceneManager.GetActiveScene().name == "SpaceInvaders")
      {
         if (enemies.Count == 0)
         {
            GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>().LoadGameScene("Credits");
         }
         MoveChildren();
         if (ChildHitsWall())
         {
            MoveChildrenDown();
         }
      }
   }

   void SpawnUFO()
   {
      GameObject ufo = Instantiate(enemyPrefab, new Vector3(-maxDistance, gridHeight + 1, 0) * 1.5f,
         Quaternion.identity);
      ufo.tag = "UFO";
      ufo.GetComponent<SpriteRenderer>().sprite = ufoSprite;
      ufo.GetComponent<Enemy>().score = Random.Range(50,300);
      enemies.Add(ufo);
   }
   void MoveChildren() {
      foreach(GameObject enemy in enemies) {
         if (!enemy.CompareTag("UFO"))
         {
            enemy.transform.Translate(direction * (speed * Time.deltaTime));
         }
         if(enemy.CompareTag("UFO"))
         {
            enemy.transform.Translate(Vector3.right * (7.5f * Time.deltaTime));
            if (enemy.transform.position.x > maxDistance+2)
            {
               enemy.GetComponent<Enemy>().score = 0;
               Destroy(enemy.gameObject);
            }
         }
      }
   }
   
   bool ChildHitsWall() {
      foreach(GameObject enemy in enemies) {
         if(Math.Abs(enemy.transform.position.x) >= maxDistance && !enemy.CompareTag("UFO")) {
            return true;
         }
      }
      return false;
   }

   void MoveChildrenDown() {
      // We must include internal horizontal shift as an atomic instruction for descent
      // Without it, there is a chance that multiple invocations will occur, causing multiple steps down
      direction *= -1;
      foreach(GameObject enemy in enemies) {
         if (!enemy.CompareTag("UFO"))
         {
            enemy.transform.Translate(direction + new Vector3(speed * Time.deltaTime, -1, 0));
         }
      }
   }

   void CleanSlate()
   {
      foreach (GameObject enemy in enemies)
      {
         enemy.GetComponent<Enemy>().score = 0;
         Destroy(enemy);
      }
      Destroy(gameObject);
   }
}
