using UI.Base_Classes;

namespace UI
{
    // MainMenuButton class extends ButtonController
    public class MainMenuButton : ButtonController
    {
        // Start method is called when the script instance is being loaded
        private void Start()
        {
            // This sets up the button to trigger loading the main menu scene when clicked
            Button.onClick.AddListener(GameManager.Instance.LoadMainMenu);
        }

        protected override bool IsInteractable()
        {
            // The main menu button is always interactable
            return true;
        }
    }
}