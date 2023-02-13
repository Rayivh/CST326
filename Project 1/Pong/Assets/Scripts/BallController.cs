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
    public GameObject scoreTextObject;
    
    // Physics
    public bool incrementForce = true;
    private float _force = 1000f;
    private Rigidbody _rb;
    
    // Score
    private int score1 = 0;
    private int score2 = 0;
    public TextMeshProUGUI scoreText;
    
    // Audio
    public AudioClip paddleClip;
    public AudioClip wallClip;
    public AudioClip startClip;

    private void Start()
    {
        // Physics Init
        _rb= GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _rb.AddForce(new Vector3(1,1,0)* 1000f, ForceMode.Force);
        
        // Score Init
        score1 = 0;
        score2 = 0;
        scoreText.text = $"Player1:{score1.ToString()} Player1:{score2.ToString()}";
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGameObject = other.gameObject;
        
        // Hits paddle
        if (otherGameObject.CompareTag("Paddle"))
        {

            
            // --Physics------------------------------------------------------------------------------------------------
            
            // Fetch paddle data
            DemoPaddle demoPaddle = otherGameObject.GetComponent<DemoPaddle>();
            bool player1 = demoPaddle.player1;

            // Calculate Pong-Accurate Trajectory (Contact height determines exit angle)
            float heightDif = otherGameObject.transform.position.y - transform.position.y;
            float angleVariance = heightDif * (player1 ? -45f : 45f);
            Vector3 bounceDirection = Quaternion.Euler(0f, 0f, angleVariance) * (player1 ? Vector3.right : Vector3.left);
            
            // Apply Physics Update
            if (incrementForce)
            {
                _force += 1000f;
                //Debug.Log($"Force is now {_force.ToString("F2")}");
            }
            _rb.velocity = Vector3.zero;
            _rb.AddForce(bounceDirection * _force, ForceMode.Force);
            
            
            // --Audio--------------------------------------------------------------------------------------------------
            // Play a Sound that fluctuates depending on angle variance from 0 (perpendicular inwards)
            float ABSangleVariance = Mathf.Abs(angleVariance);
            GetComponent<AudioSource>().pitch *= ABSangleVariance/10;
            GetComponent<AudioSource>().PlayOneShot(paddleClip);
        }
        // Wall Collision
        else
        {
            GetComponent<AudioSource>().pitch = 1.0f;
            GetComponent<AudioSource>().PlayOneShot(wallClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        string otherTag = otherGameObject.tag;
        string otherName = otherGameObject.name;
        
        switch (otherTag)
        {
            // Goal ====================================================================================================
            case "Goal": // Hits the Goal
                string scoringPlayer = "", otherPlayer = "";
                _force = 1000f;
                _rb.velocity = Vector3.zero;
                transform.position = Vector3.zero;
                Vector3 startDirection = Vector3.zero;
                switch (otherName)
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

                // Score Update -----------------------------------------------
                if(score1 == 11 || score2 == 11)
                {
                    scoreText.text = $"Game Over, {scoringPlayer} Wins";
                    score1 = 0;
                    score2 = 0;
                }
                else
                {
                    scoreText.text = $"Player1:{score1.ToString()} Player2:{score2.ToString()}";
                    scoreText.transform.localScale += new Vector3(0.1f,0.1f,0);
                    scoreText.color = Color.yellow;
                    
                    IEnumerator ScoreScale()
                    {
                        yield return new WaitForSeconds(0.5f);
                        scoreText.transform.localScale -= new Vector3(0.1f,0.1f,0);
                        scoreText.color = Color.white;
                    }
                    StartCoroutine(ScoreScale());
                }
                
                
                // Audio Event  -----------------------------------------------
                GetComponent<AudioSource>().pitch = 1.0f;
                GetComponent<AudioSource>().PlayOneShot(startClip);
            
                Debug.Log($"{scoringPlayer} just scored on {otherPlayer}");
                Debug.Log($"The score is now P1:{score1.ToString()} : P2:{score2.ToString()}");

                _rb.AddForce(startDirection * 1000f, ForceMode.Force);
                break;
            
            
            
            // Power Up ================================================================================================
            case "PowerUp": // Hits a PowerUp
                IEnumerator PowerUpRespawn()
                {
                    yield return new WaitForSeconds(5);
                    other.gameObject.SetActive(true);
                }
                StartCoroutine(PowerUpRespawn());
                
                switch (otherName)
                {
                    case "Reverse":
                        _rb.velocity = -_rb.velocity;
                        break;
                    
                    case "Plateau":
                        _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
                        break;
                }
                other.gameObject.SetActive(false);
                break;
        }
    }
}
