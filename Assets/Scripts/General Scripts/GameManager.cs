using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    [NonSerialized] public ScrollSlipManager SlipManager;
    [NonSerialized] public bool gameOver = false;
    [NonSerialized] public bool tutorialMode = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        SlipManager = GetComponent<ScrollSlipManager>();


        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameEventHandler.Instance.DestroyedObject += OnDestroyObject;
        ResumeGame();
    }

    public void RestartScene()
    {
        gameOver = false;
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            GameOverStateTrigger();
            return;
        }
    }

    private void GameOverStateTrigger()
    {
        Time.timeScale = 0;
        GameEventHandler.Instance.TriggerGameOver();
    }

    private void OnDestroyObject(GameObject obj)
    {
        Product product = obj.GetComponent<Product>();
        if (product != null)
        {
            try
            {
                product.gameObject.transform.position = new Vector3(-100, 0, 2);
                product.ObjectPool.Release(product);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
        else
        {
            Debug.Log(System.DateTime.Now + " Destroyed an object");
            Destroy(obj.gameObject);
        }
    }
}
