using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisclaimerText : MonoBehaviour
    {
        public Slider widthSlider;
        public Slider heightSlider;

        private void Start()
        {
            widthSlider.onValueChanged.AddListener(delegate { EnableText(); });
            heightSlider.onValueChanged.AddListener(delegate { EnableText(); });
            EnableText();
        }

        private void EnableText()
        {
            if (widthSlider.value > 50 || heightSlider.value > 50)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
