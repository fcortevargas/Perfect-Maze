using UI.Base_Classes;

namespace UI
{
    // SpeedSlider class extends SliderController
    public class WidthSlider : SliderController
    { 
        protected override void HandleSliderValueChanged(float value)
        {
            // Set the MazeHeight in the GameManager to the current value of the slider
            GameManager.MazeWidth = (int)value;
        }
    }
}
