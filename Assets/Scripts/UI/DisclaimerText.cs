using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisclaimerText : MonoBehaviour
    {
        // Reference to the width slider UI element
        public Slider widthSlider;
        // Reference to the height slider UI element
        public Slider heightSlider;
        
        private void Start()
        {
            // Add listeners to the width and height sliders that call EnableText method whenever their value changes
            widthSlider.onValueChanged.AddListener(delegate { EnableText(); });
            heightSlider.onValueChanged.AddListener(delegate { EnableText(); });

            // Ensure the correct visibility of the text is set at the start based on the current slider values
            EnableText();
        }
        
        private void EnableText()
        {
            // Check if either slider's value is greater than 50
            if (widthSlider.value > 50 || heightSlider.value > 50)
            {
                // If so, enable this GameObject
                gameObject.SetActive(true);
            }
            else
            {
                // Otherwise, disabl this GameObject
                gameObject.SetActive(false);
            }
        }
    }
}