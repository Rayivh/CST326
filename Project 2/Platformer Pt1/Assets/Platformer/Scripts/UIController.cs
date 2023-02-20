using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timerText, coinText, scoreText;

    public int time;
    public int coins;
    public int score;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time: " + (time - (int)Time.realtimeSinceStartup).ToString("F0");
        coinText.text = $"Coins: {coins}";
        scoreText.text = $"Score: {score}";
    }
}
