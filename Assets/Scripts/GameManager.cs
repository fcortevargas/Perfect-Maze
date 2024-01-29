using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Create a singleton instance
    public static GameManager Instance { get; private set; } 
    
    public static int MazeWidth { get; set; } = 10;
    public static int MazeHeight { get; set; } = 10;
    public static float MazeSpeed { get; set; }

    public static bool IsMazeCompleted { get; set; }
    public static bool IsMazeReset { get; set; }
    public static bool IsMazeSceneLoaded { get; private set; }
    public static bool IsGameSceneLoaded { get; private set; }

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
        MazeSpeed = 0;
        MazeWidth = 10;
        MazeHeight = 10;
        IsMazeSceneLoaded = false;
        IsGameSceneLoaded = false;
    }

    public void LoadMaze()
    {
        SceneManager.LoadScene("Maze");
        IsMazeSceneLoaded = true;
        IsGameSceneLoaded = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        IsGameSceneLoaded = true;
    }
}
