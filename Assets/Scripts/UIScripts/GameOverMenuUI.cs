using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] private Transform gameOverScreen;

    private void Start()
    {
        GameEventHandler.Instance.TriggeredGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        if (gameOverScreen == null) return;

        gameOverScreen.gameObject.SetActive(true);
    }
}
