using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneSwitcher : MonoBehaviour
{
    private static Dictionary<string, GameObject> _instances = new();
    public string ID;
    public AudioClip background;
    public AudioClip gameStart;
    void Awake()
    {
        if (_instances.ContainsKey(ID))
        {
            var existing = _instances[ID];
            if(existing != null)
            {
                if(ReferenceEquals(gameObject, existing))
                {
                    return;
                }
                Destroy(gameObject);
                return;
            }
        }

        _instances[ID] = gameObject;
        DontDestroyOnLoad(gameObject);
    }
    
    // void Start()
    // {
    //     Scene[] scenes = new Scene[SceneManager.sceneCount];
    //     
    //     for (int i = 0; i < SceneManager.sceneCount; i++)
    //     {
    //         scenes[i] = SceneManager.GetSceneAt(i);
    //     }
    //     foreach (Scene s in scenes)
    //     {
    //         if (s.name != "Main Menu")
    //         {
    //             SceneManager.UnloadSceneAsync(s);
    //         }
    //     }
    // }

    public void LoadGameScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Credits":
                GameObject.Find("EnemyGrid").GetComponent<EnemyManager>().Invoke("CleanSlate", 0f);
                StartCoroutine(CreditsScreen());
                break;
            
            case "SpaceInvaders":
                //Don't destroy the grid/children for this scene-transition
                GetComponent<AudioSource>().PlayOneShot(background);
                GetComponent<AudioSource>().PlayOneShot(gameStart);
                EnemyManager enemyGrid = GameObject.Find("EnemyGrid").GetComponent<EnemyManager>();
                enemyGrid.InvokeRepeating("SpawnUFO", 1f, 15f);
                DontDestroyOnLoad(enemyGrid);
                foreach (GameObject enemy in enemyGrid.enemies)
                {
                    DontDestroyOnLoad(enemy);
                    if (enemy.CompareTag("Shooter"))
                    {
                        enemy.GetComponent<Enemy>().Invoke("FireProjectile", Random.Range(enemyGrid.fireRateMin, enemyGrid.fireRateMax));
                    }
                }
                break;
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    void SwitchStart()
    {
        LoadGameScene("SpaceInvaders");
    }
    
    void SwitchCredits()
    {
        LoadGameScene("Credits");
    }
    
    IEnumerator CreditsScreen()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
    }
}
