using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        try
        {
            GameObject gameManager;
            GameObject player;
            gameManager = FindObjectOfType<GameManager>().gameObject;
            player = FindObjectOfType<PlayerController>().gameObject;
            if (gameManager != null)
            {
                Destroy(gameManager);
            }
            if(player != null)
            {
                Destroy(player);
            }
        }
        catch { }
    }

    public void GoToTutorialStage()
    {
        SceneManager.LoadScene("Tutorial Level");
    }

    public void GoToNormalStage()
    {
        SceneManager.LoadScene("Normal Level");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
