using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerUI : MonoBehaviour
{
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void ExitSession()
    {
        GameManager.Instance.ExitSession();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void RestartScene()
    {
        GameManager.Instance.RestartScene();
    }
}
