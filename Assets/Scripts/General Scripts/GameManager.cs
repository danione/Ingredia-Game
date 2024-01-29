using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    public bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameEventHandler.Instance.UpgradesMenuOpen += OnUpdatesMenuOpened;
        GameEventHandler.Instance.UpgradesMenuClose += OnUpdatesMenuClosed;
        GameEventHandler.Instance.DestroyedObject += OnDestroyObject;
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    public void ExitSession()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
        }
    }

    private void OnUpdatesMenuOpened()
    {
        PauseGame();
    }

    private void OnUpdatesMenuClosed()
    {
        ResumeGame();
    }

    private void OnDestroyObject(GameObject obj)
    {
        Product product = obj.GetComponent<Product>();
        if (product != null)
        {
            try
            {
                product.ObjectPool.Release(product);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }
}
