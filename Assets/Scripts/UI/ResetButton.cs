namespace UI
{
    public class ResetButton : ButtonController
    { 
        private void Start()
        {
            if (GameManager.IsMazeSceneLoaded)
                Button.onClick.AddListener(Maze.Instance.ResetMaze);
        }
        protected override bool IsInteractable()
        {
            return GameManager.IsMazeCompleted && !GameManager.IsMazeReset;
        }
    }
}
