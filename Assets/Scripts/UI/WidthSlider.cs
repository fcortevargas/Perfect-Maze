namespace UI
{
    public class WidthSlider : SliderController
    { 
        protected override void HandleSliderValueChanged(float value)
        {
            GameManager.MazeWidth = (int)value;
        }
    }
}
