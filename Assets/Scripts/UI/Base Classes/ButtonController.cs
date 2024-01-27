using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonController : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
    
        private void Update()
        {
            _button.interactable = IsInteractable(); 
        }

        protected abstract bool IsInteractable();
    }
}
