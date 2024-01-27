using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Create a singleton instance
    public static GameManager Instance { get; private set; } 
    
    public static int MazeWidth { get; set; } = 10;
    public static int MazeHeight { get; set; } = 10;
    public static float MazeSpeed { get; set; }
    
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

    public void StartGame()
    {
        SceneManager.LoadScene("Maze");
    }
}
