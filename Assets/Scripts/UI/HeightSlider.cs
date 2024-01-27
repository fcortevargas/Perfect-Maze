namespace UI
{
    public class HeightSlider : SliderController
    {
        protected override void HandleSliderValueChanged(float value)
        {
            GameManager.MazeHeight = (int)value;
        }
    }
}
