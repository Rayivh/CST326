using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public AudioClip background;
    public AudioClip gameStart;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        Scene[] scenes = new Scene[SceneManager.sceneCount];
        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
        }
        foreach (Scene s in scenes)
        {
            if (s.name != "Main Menu")
            {
                SceneManager.UnloadSceneAsync(s);
            }
        }
        GetComponent<AudioSource>().PlayOneShot(background);

    }

    public void LoadGameScene(string sceneName)
    {
        if (sceneName == "Credits")
        {
            GameObject.Find("EnemyGrid").GetComponent<EnemyManager>().Invoke("CleanSlate", 0f);
            StartCoroutine(CreditsScreen());
        }

        if (sceneName == "SpaceInvaders")
        {
            GetComponent<AudioSource>().PlayOneShot(gameStart);
            EnemyManager enemyGrid = GameObject.Find("EnemyGrid").GetComponent<EnemyManager>();
            
            enemyGrid.InvokeRepeating("SpawnUFO", 1f, 15f);
            foreach (GameObject enemy in enemyGrid.enemies)
            {
                if (enemy.CompareTag("Shooter"))
                {
                    enemy.GetComponent<Enemy>().Invoke("FireProjectile", Random.Range(enemyGrid.fireRateMin, enemyGrid.fireRateMax));
                }
            }
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    IEnumerator CreditsScreen()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
    }
}
