using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using TMPro;
public class BallController : MonoBehaviour
{
    private int score1 = 0;
    private int score2 = 0;
    public TextMeshProUGUI scoreText;
    public GameObject scoreTextObject;
    

    public bool incrementForce = true;
    private float _force = 1000f;
    private Rigidbody _rb;
    
    private void Start()
    {
        score1 = 0;
        score2 = 0;
        scoreText.text = $"Player1:{score1.ToString()} Player1:{score2.ToString()}";
            
        _rb= GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _rb.AddForce(new Vector3(1,1,0)* 1000f, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided!");
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Paddle"))
        {
            // Get data from other script (Paddle)
            DemoPaddle demoPaddle = otherGameObject.GetComponent<DemoPaddle>();
            bool player1 = demoPaddle.player1;
            
            // Calculate Pong-Accurate Trajectory (Contact height determines exit angle)
            float heightDif = otherGameObject.transform.position.y - transform.position.y;
            Vector3 bounceDirection = Quaternion.Euler(0f, 0f, heightDif == 0 ? 0f : heightDif * (player1 ? -45f : 45f)) * (player1 ? Vector3.right : Vector3.left);
            Debug.Log($"Bouncing by {(heightDif * (player1 ? -45f : 45f)).ToString("F2")} Degrees");

            // Apply Physics Update
            if (incrementForce)
            {
                _force += 1000f;
                Debug.Log($"Force is now {_force.ToString("F2")}");
            }
            _rb.velocity = Vector3.zero;
            _rb.AddForce(bounceDirection * _force, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered! Force is reset");
        // On_Score: Restart
        string scoringPlayer = "", otherPlayer = "";
        _force = 1000f;
        _rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        Vector3 startDirection = Vector3.zero;
        switch (other.gameObject.name)
        {
            case "Left":
                startDirection = Vector3.left;
                scoringPlayer = "Player2";
                otherPlayer = "Player1";
                score2++;
                break;
        
            case "Right":
                startDirection = Vector3.right;
                scoringPlayer = "Player1";
                otherPlayer = "Player2";
                score1++;
                break;
        }
        Debug.Log($"{scoringPlayer} just scored on {otherPlayer}");
        Debug.Log($"The score is now P1:{score1.ToString()} : P2:{score2.ToString()}");
        
        if(score1 == 11 || score2 == 11)
        {
            scoreText.text = $"Game Over, {scoringPlayer} Wins";
            score1 = 0;
            score2 = 0;
        }
        else
        {
            scoreText.text = $"Player1:{score1.ToString()} Player2:{score2.ToString()}";
        }
        _rb.AddForce(startDirection * 1000f, ForceMode.Force);
        
        
    }
}
