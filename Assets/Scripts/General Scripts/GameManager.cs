using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    private int playerScore = 0;
    public bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreUI();
        if(playerScore < 0)
        {
            gameOver = true;
            Debug.Log("Game Over");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + playerScore.ToString();
    }
}
