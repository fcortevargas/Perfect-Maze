namespace UI
{
    public class MainMenuButton : ButtonController
    {
        private void Start()
        {
            Button.onClick.AddListener(GameManager.Instance.LoadMainMenu);
        }

        protected override bool IsInteractable()
        {
            return true;
        }
    }
}



