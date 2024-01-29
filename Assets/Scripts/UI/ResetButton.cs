using UI.Base_Classes;

namespace UI
{
    // ResetButton class extends ButtonController
    public class ResetButton : ButtonController
    { 
        private void Start()
        {
            // If the Maze scene is loaded, add a listener to the button's onClick event to call the ResetMaze method in the Maze instance
            if (GameManager.IsMazeSceneLoaded)
                Button.onClick.AddListener(Maze.Instance.ResetMaze);
        }
        
        protected override bool IsInteractable()
        {
            // The button is interactable only if the maze is completed and has not been reset yet
            return GameManager.IsMazeCompleted && !GameManager.IsMazeReset;
        }
    }
}