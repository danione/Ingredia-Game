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
            gameManager = FindObjectOfType<GameManager>().gameObject;
            if (gameManager != null)
            {
                Destroy(gameManager);
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
