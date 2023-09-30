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

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
        }
    }

}
