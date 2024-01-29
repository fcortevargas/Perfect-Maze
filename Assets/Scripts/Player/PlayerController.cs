using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 1f;
    
        private void Update()
        {
            HandleMovement();
            ExitGame();
        }

        private void HandleMovement()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
        
            transform.position += new Vector3(horizontalInput, verticalInput, 0) * (speed * Time.deltaTime);
        }

        private void ExitGame()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                GameManager.Instance.LoadMainMenu();
        }
    }
}
