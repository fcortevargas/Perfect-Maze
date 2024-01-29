using UnityEngine;

namespace UI
{
    public class StartButton : ButtonController
    {
        private void Start()
        {
            if (!GameManager.IsMazeSceneLoaded)
                Button.onClick.AddListener(GameManager.Instance.LoadMaze);
            else
                Button.onClick.AddListener(Maze.Instance.StartDisablingWalls);
        }

        protected override bool IsInteractable()
        {
            if (Maze.Instance == null)
                return GameManager.MazeSpeed > 0;
            
            return GameManager.IsMazeReset;
        }
    }
}
