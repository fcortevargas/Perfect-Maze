using UI.Base_Classes;

namespace UI
{
    // GameButton class extends ButtonController
    public class GameButton : ButtonController
    {
        private void Start()
        {
            // This sets up the button to trigger the game loading process when clicked
            Button.onClick.AddListener(GameManager.Instance.LoadGame);
        }
        
        // This method defines the conditions under which the button is interactable
        protected override bool IsInteractable()
        {
            // The button is interactable only if the maze is completed and both the width and height of the maze are less than or equal to 50
            return GameManager.IsMazeCompleted && GameManager.MazeWidth <= 50 && GameManager.MazeHeight <= 50;
        }
    }
}