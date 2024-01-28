using UnityEngine;

namespace UI
{
    public class StartButton : ButtonController
    {
        private void Start()
        {
            if (!GameManager.IsGameStarted)
                Button.onClick.AddListener(GameManager.Instance.StartGame);
        }

        protected override bool IsInteractable()
        {
            if (GameObject.Find("Maze") == null)
                return GameManager.MazeSpeed > 0;
            
            return GameManager.IsMazeReset;
        }
    }
}
