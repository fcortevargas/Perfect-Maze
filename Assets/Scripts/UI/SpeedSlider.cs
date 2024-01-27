namespace UI
{
    public class SpeedSlider : SliderController
    {
        protected override void HandleSliderValueChanged(float value)
        {
            GameManager.MazeSpeed = value;
        }
    }
}
