using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager to ensure only one instance exists
    public static GameManager Instance { get; private set; } 
    
    // Static properties for the maze's dimensions and speed
    public static int MazeWidth { get; set; } = 10; // Default maze width
    public static int MazeHeight { get; set; } = 10; // Default maze height
    public static float MazeSpeed { get; set; } // Speed of maze generation

    // Static flags to track the state of the maze and scenes
    public static bool IsMazeCompleted { get; set; } // Flag to indicate if the maze is completed
    public static bool IsMazeReset { get; set; } // Flag to indicate if the maze is reset
    public static bool IsMazeSceneLoaded { get; private set; } // Flag to indicate if the maze scene is loaded
    public static bool IsGameSceneLoaded { get; private set; } // Flag to indicate if the game scene is loaded

    private void Awake()
    {
        // Implement the Singleton pattern to ensure only one GameManager instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent the GameManager from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances of GameManager
        }
    }
    
    // Method to load the main menu scene and reset game settings
    public void LoadMainMenu()
    {
        // Load the main menu (start) scene
        SceneManager.LoadScene("Start"); 
        
        // Reset game settings to default values
        MazeSpeed = 0;
        MazeWidth = 10;
        MazeHeight = 10;
        
        // Update scene load flags
        IsMazeSceneLoaded = false;
        IsGameSceneLoaded = false;
    }

    // Method to load the maze scene and update relevant flags
    public void LoadMaze()
    {
        // Load the maze scene
        SceneManager.LoadScene("Maze"); 
        
        // Update scene load flags
        IsMazeSceneLoaded = true;
        IsGameSceneLoaded = false;
    }

    // Method to load the game scene and update relevant flags
    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); // Load the game scene
        // Update scene load flags
        IsGameSceneLoaded = true;
    }
}
