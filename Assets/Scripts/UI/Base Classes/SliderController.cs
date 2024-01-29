using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base_Classes
{
    // SliderController is an abstract class that provides a base implementation for slider controls in the UI
    public abstract class SliderController : MonoBehaviour
    {
        private Slider _slider; // Reference to the Slider component on this GameObject
        public TextMeshProUGUI valueText; // Reference to the TextMeshProUGUI component for displaying the slider value
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        private void Start()
        {
            // Add a listener to the slider's onValueChanged event to handle changes in the slider's value
            _slider.onValueChanged.AddListener(HandleSliderValueChanged);

            // Add a listener to update the displayed value when the slider value changes
            _slider.onValueChanged.AddListener(UpdateValueDisplay);

            // Initialize the value display with the current slider value
            UpdateValueDisplay(_slider.value);
        }

        // Abstract method to be implemented by derived classes to define how slider value changes should be handled
        protected abstract void HandleSliderValueChanged(float value);
        
        private void UpdateValueDisplay(float value)
        {
            // Check if the valueText is assigned and update its text to reflect the current slider value
            if (valueText != null)
            {
                valueText.text = value.ToString();
            }
        }
    }
}