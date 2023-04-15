using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    void Start()
    {
        EnemiesAlive = 0;
    }
    public Wave[] waves;
    
    public Transform spawnPoint;
    
    public float waveInterval = 5f;
    public TextMeshProUGUI waveTimerText;

    public GameManager gameManager;
    
    private float countdown = 2f;
    private int waveNumber;
    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        
        if (waveNumber == waves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = waveInterval;
            return;
        }

        countdown -= Time.deltaTime;
        
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveTimerText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];
        
        EnemiesAlive = wave.count;
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }
        
        waveNumber++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
