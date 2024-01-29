using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Speed of the player movement
        public float speed = 1f;
        
        private void Update()
        {
            // Handle player movement based on input
            HandleMovement();

            // Check for and handle the exit game command
            ExitGame();
        }

        // HandleMovement processes player input for movement
        private void HandleMovement()
        {
            // Get horizontal and vertical input from the keyboard
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
        
            // Update the player's position based on input and speed
            transform.position += new Vector3(horizontalInput, verticalInput, 0) * (speed * Time.deltaTime);
        }

        // ExitGame checks for the exit command and triggers the game's exit or main menu loading
        private static void ExitGame()
        {
            // If the Q key is pressed, call the LoadMainMenu method from GameManager
            if (Input.GetKeyDown(KeyCode.Q))
                GameManager.Instance.LoadMainMenu();
        }
    }
}