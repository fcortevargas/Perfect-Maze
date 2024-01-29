using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    public float zoomSpeed = 1f;
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 200f;
    public float panSpeed = 0.5f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        PositionCamera();
    }

    private void PositionCamera()
    {
        var x = (float)GameManager.MazeWidth;
        var y = (float)GameManager.MazeHeight;
        gameObject.transform.position = new Vector3(x / 2 - 0.5f, y / 2 - 0.5f, -10);
        
        if (_camera.aspect >= x / y)
        {
            _camera.orthographicSize = y / 2 + y / 4;
        }
        else
        {
            var differenceInSize = x / y / _camera.aspect;
            _camera.orthographicSize = y / 2 * differenceInSize + y / 4;;
        }
    }

    private void HandleZoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        
        var orthographicSize = _camera.orthographicSize;
        orthographicSize -= scroll * zoomSpeed;
        
        _camera.orthographicSize = orthographicSize;
        _camera.orthographicSize = Mathf.Clamp(orthographicSize, minOrthographicSize, maxOrthographicSize);
    }

    private void HandlePan()
    {
        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            var moveX = -Input.GetAxis("Mouse X") * panSpeed;
            var moveY = -Input.GetAxis("Mouse Y") * panSpeed;
            _camera.transform.Translate(new Vector3(moveX, moveY, 0), Space.World);
        }
    }

    private void Update()
    {
        HandleZoom();
        HandlePan();
    }
}
