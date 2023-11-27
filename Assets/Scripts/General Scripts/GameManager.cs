using UnityEngine;


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
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
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

}
