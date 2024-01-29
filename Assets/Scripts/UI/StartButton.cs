using UI.Base_Classes;

namespace UI
{
    // The StartButton class extends ButtonController
    public class StartButton : ButtonController
    {
        private void Start()
        {
            // If the Maze scene is not loaded yet, add a listener to the button's onClick event to load the Maze scene
            if (!GameManager.IsMazeSceneLoaded)
                Button.onClick.AddListener(GameManager.Instance.LoadMaze);
            // If the Maze scene is already loaded, add a listener to start disabling the walls in the Maze
            else
                Button.onClick.AddListener(Maze.Instance.StartDisablingWalls);
        }
        
        // This method defines the conditions under which the button is interactable
        protected override bool IsInteractable()
        {
            // If the Maze instance is not available, the button is interactable only if the maze generation speed is set
            if (Maze.Instance == null)
                return GameManager.MazeSpeed > 0;
            
            // If the Maze instance is available, the button is interactable if the maze is in a reset state
            return GameManager.IsMazeReset;
        }
    }
}