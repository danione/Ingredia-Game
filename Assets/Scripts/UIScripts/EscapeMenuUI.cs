using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject controlsMenu;

    // Start is called before the first frame update
    void Start()
    {
        PlayerEventHandler.Instance.EscapeMenuOpened += OnEscapeMenuOpened;
        PlayerEventHandler.Instance.ClosedAllOpenMenus += OnCloseMenu;
    }

    private void OnDestroy()
    {
        PlayerEventHandler.Instance.EscapeMenuOpened -= OnEscapeMenuOpened;
        PlayerEventHandler.Instance.ClosedAllOpenMenus -= OnCloseMenu;
    }

    private void OnCloseMenu()
    {
        escapeMenu.SetActive(false);
    }

    private void OnEscapeMenuOpened()
    {
        if(escapeMenu.activeSelf == false)
        {
            controlsMenu.SetActive(false);
            GameManager.Instance.PauseGame();
        }
        else
        {
            controlsMenu.SetActive(false);
            PlayerEventHandler.Instance.CloseAMenu();
            GameManager.Instance.ResumeGame();
        }

        escapeMenu.SetActive(!escapeMenu.activeSelf);
    }
}
