using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class SliderController : MonoBehaviour
    {
        private Slider _slider;
        public TextMeshProUGUI valueText;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.onValueChanged.AddListener(HandleSliderValueChanged);
            _slider.onValueChanged.AddListener(UpdateValueDisplay);
            UpdateValueDisplay(_slider.value);
        }

        protected abstract void HandleSliderValueChanged(float value);

        private void UpdateValueDisplay(float value)
        {
            if (valueText != null)
            {
                valueText.text = value.ToString();
            }
        }
    }
}