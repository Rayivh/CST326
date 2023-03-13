using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button start, credits;
    void Start()
    {
        start.onClick.AddListener(startHook);
        credits.onClick.AddListener(creditsHook);
    }
    
    void startHook()
    {
        GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>().Invoke("SwitchStart",0f);
    }
    void creditsHook()
    {
        GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>().Invoke("SwitchCredits",0f);
    }
}
