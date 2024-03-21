using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject wonGameScreen;
    private static UIDisplayCanvas instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        GameEventHandler.Instance.WonCondition += OnWinCondition;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.WonCondition -= OnWinCondition;
    }

    private void OnWinCondition()
    {
        wonGameScreen.SetActive(true);
        GameManager.Instance.PauseGame();
        PlayerInputHandler.permissions.LockAll();
    }
}
