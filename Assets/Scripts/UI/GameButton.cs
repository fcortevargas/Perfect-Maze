using UnityEngine;

namespace UI
{
    public class GameButton : ButtonController
    {
        private void Start()
        {
            Button.onClick.AddListener(GameManager.Instance.LoadGame);
        }

        protected override bool IsInteractable()
        {
            return GameManager.IsMazeCompleted && GameManager.MazeWidth <= 50 && GameManager.MazeHeight <= 50;
        }
    }
}
