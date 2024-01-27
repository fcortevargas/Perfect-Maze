namespace UI
{
    public class StartButton : ButtonController
    {
        protected override bool IsInteractable()
        {
            return GameManager.MazeSpeed > 0;
        }
    }
}
