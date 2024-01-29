using System.Collections;
using UnityEngine;

namespace UI
{
    public class InstructionsText : MonoBehaviour
    {
        // Duration for which the instructions text will be visible (in seconds)
        public float timeActive = 10f;
        
        private void Start()
        {
            // Start the coroutine to disable the instructions text after a specified duration
            StartCoroutine(DisableInstructions());
        }

        // Coroutine that handles the delayed deactivation of the instructions text
        private IEnumerator DisableInstructions()
        {
            // Wait for the specified duration
            yield return new WaitForSeconds(timeActive);

            // After waiting, deactivate this GameObject
            gameObject.SetActive(false);
        }
    }
}