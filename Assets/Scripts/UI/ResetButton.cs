namespace UI
{
    public class ResetButton : ButtonController
    { 
        protected override bool IsInteractable()
        {
            return GameManager.IsMazeCompleted && !GameManager.IsMazeReset;
        }
    }
}
