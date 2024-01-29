using UnityEngine;
using UnityEngine.UI;

namespace UI.Base_Classes
{
    // ButtonController is an abstract class that provides a base implementation for button controls in the UI
    public abstract class ButtonController : MonoBehaviour
    {
        // Protected field for the Button component
        protected Button Button;
        
        private void Awake()
        {
            Button = GetComponent<Button>();
        }
        
        private void Update()
        {
            // Calls the abstract IsInteractable method, which is implemented in derived classes
            Button.interactable = IsInteractable(); 
        }
        
        // Abstract method to determine whether the button is interactable based on custom conditions
        protected abstract bool IsInteractable();
    }
}