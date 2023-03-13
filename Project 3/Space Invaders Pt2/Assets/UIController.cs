using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText, highScoreText;
    
    
    public int score;
    public int highScore;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.OnDeath += UpdateScore;
        highScore = int.Parse(PlayerPrefs.GetString("score", "0"));
        highScoreText.text = highScore.ToString("D4");
    }

    void UpdateScore(GameObject enemy)
    {
        score += enemy.GetComponent<Enemy>().score;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString("D4");
            PlayerPrefs.SetString("score", highScore.ToString("D4"));
            PlayerPrefs.Save();
        }
        scoreText.text = score.ToString("D4");
    }
}