using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
   public List<GameObject> enemies;
   public GameObject enemyPrefab;

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

      //init ufo
      InvokeRepeating("SpawnUFO", 1f, 15f);
      //init grid
      for (int i = 0; i < gridHeight; i++)
      {
         for (int j = 0; j < gridWidth; j++)
         {
            GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(i, j, 0) * 1.5f, Quaternion.identity, transform);
            if (j == (int) gridWidth - 1)
            {
               enemy.tag = "Shooter";
            }
            else if (j == (int) gridWidth - 2)
            {
               enemy.tag = "Crab";
            }
            else
            {
               enemy.tag = "Jelly";
            }
            enemies.Add(enemy);
         }
      }
   }

   void CleanCorpse(GameObject enemy)
   {
      if(!enemy.CompareTag("UFO"))
      {
         enemies.Remove(enemy);
         speed+=deathIncrement;
      }
   }
   
   void Update() {
      MoveChildren();
      if (ChildHitsWall())
      {
         MoveChildrenDown(); // The name isn't just for humor, its a note to remember how to handle groups of children
      }
   }

   void SpawnUFO()
   {
      GameObject ufo = Instantiate(enemyPrefab, new Vector3(-maxDistance, gridHeight + 1, 0) * 1.5f,
         Quaternion.identity);
      ufo.tag = "UFO";

   }
   void MoveChildren() {
      foreach(GameObject enemy in enemies) {
         enemy.transform.Translate(direction * (speed * Time.deltaTime));
      }
   }
   
   bool ChildHitsWall() {
      foreach(GameObject enemy in enemies) {
         if(Math.Abs(enemy.transform.position.x) >= maxDistance) {
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
         enemy.transform.Translate(direction + new Vector3(speed * Time.deltaTime, -1, 0));
      }
   }
}
