using UnityEngine;

public class LogFileSaver : MonoBehaviour
{
    private void Start()
    {
        Application.logMessageReceived += LogToFileHandler;
        DontDestroyOnLoad(gameObject);
    }

    private void LogToFileHandler(string condition, string stackTrace, LogType type)
    {
        // Append the log information to a file
        System.IO.File.AppendAllText("LogFile.txt", $"{type}: {condition}\n{stackTrace}\n\n");
    }
}
