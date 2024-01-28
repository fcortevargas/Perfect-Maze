using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonController : MonoBehaviour
    {
        protected Button Button;
        
        private void Awake()
        {
            Button = GetComponent<Button>();
        }
    
        private void Update()
        {
            Button.interactable = IsInteractable(); 
        }

        protected abstract bool IsInteractable();
    }
}
