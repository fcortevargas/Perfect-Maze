using UnityEngine;

namespace Cinematics
{
    // CameraController class manages the camera's behavior
    public class CameraController : MonoBehaviour
    {
        private Camera _camera; // Reference to the Camera component attached to this GameObject
        
        public float zoomSpeed = 1f; // Speed at which the camera zooms in and out
        public float minOrthographicSize = 5f; // Minimum zoom level 
        public float maxOrthographicSize = 200f; // Maximum zoom level
        public float panSpeed = 0.5f; // Speed at which the camera pans
        
        private void Awake()
        {
            // Retrieve the Camera component attached to the GameObject
            _camera = GetComponent<Camera>();
        }
        
        private void Start()
        {
            // Position the camera at the start based on the maze dimensions
            PositionCamera();
        }

        // Sets the initial position and size of the camera based on the maze dimensions
        private void PositionCamera()
        {
            // Calculate the center position of the maze
            var x = (float)GameManager.MazeWidth;
            var y = (float)GameManager.MazeHeight;
            gameObject.transform.position = new Vector3(x / 2 - 0.5f, y / 2 - 0.5f, -10);
        
            // Adjust the orthographic size of the camera based on the maze dimensions and camera aspect ratio
            if (_camera.aspect >= x / y)
            {
                _camera.orthographicSize = y / 2 + y / 4;
            }
            else
            {
                var differenceInSize = x / y / _camera.aspect;
                _camera.orthographicSize = y / 2 * differenceInSize + y / 4;
            }
        }

        // Manages the zooming functionality of the camera
        private void HandleZoom()
        {
            // Get the scroll wheel input for zooming
            var scroll = Input.GetAxis("Mouse ScrollWheel");
        
            // Adjust the orthographic size based on the input and zoom speed
            var orthographicSize = _camera.orthographicSize;
            orthographicSize -= scroll * zoomSpeed;
        
            // Clamp the orthographic size to the defined min and max values
            _camera.orthographicSize = Mathf.Clamp(orthographicSize, minOrthographicSize, maxOrthographicSize);
        }

        // Manages the panning functionality of the camera
        private void HandlePan()
        {
            // Check if the left mouse button is held down for panning
            if (Input.GetMouseButton(0))
            {
                // Calculate the movement based on mouse input and pan speed
                var moveX = -Input.GetAxis("Mouse X") * panSpeed;
                var moveY = -Input.GetAxis("Mouse Y") * panSpeed;
                // Apply the movement to the camera's position
                _camera.transform.Translate(new Vector3(moveX, moveY, 0), Space.World);
            }
        }
        
        private void Update()
        {
            HandleZoom();
            HandlePan();
        }
    }
}
