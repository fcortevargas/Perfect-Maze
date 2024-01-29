using UI.Base_Classes;

namespace UI
{
    // SpeedSlider class extends SliderController
    public class SpeedSlider : SliderController
    {
        protected override void HandleSliderValueChanged(float value)
        {
            // Set the MazeSpeed in the GameManager to the current value of the slider
            GameManager.MazeSpeed = value;
        }
    }
}