using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodCameraController : MonoBehaviour, PlayerInputs.IGodModeActions
{
    public float moveSpeed = 20; // Speed at which the camera moves
    public float borderThickness = 10f; // Thickness of the screen border for triggering movement

    public Vector2 topRightLimit = new Vector2(100, 100);
    public Vector2 bottomLeftLimit = new Vector2(-100, -100);
    private Vector3 initialPosition;
    [SerializeField] Transform cameraTargetTransform;
    private CinemachineCamera CMcamera;
    private PlayerInputs playerInputs;
    public bool isMouseMovementEnabled = false;

    void Start()
    {
        FindFirstObjectByType<PlacableManager>().SetPlayerInputs(playerInputs);
        // Store the initial position of the camera
        initialPosition = cameraTargetTransform.position;
        CMcamera = GetComponentInChildren<CinemachineCamera>();
    }

    private void Update()
    {
        if (playerInputs.GodMode.enabled)
        {
            var mousePosition = playerInputs.GodMode.Move.ReadValue<Vector2>();
            SetCameraTargetToPosition(mousePosition);
        }
    }

    void SetCameraTargetToPosition(Vector2 mouseMovement)
    {
        Vector3 mousePosition = mouseMovement;
        Vector3 moveDirection = Vector3.zero;

        // Check if the mouse is near the edges of the screen and set the move direction
        if (mousePosition.x >= Screen.width - borderThickness)
        {
            moveDirection.x += 1;
        }

        if (mousePosition.x <= borderThickness)
        {
            moveDirection.x -= 1;
        }

        if (mousePosition.y >= Screen.height - borderThickness)
        {
            moveDirection.z += 1;
        }

        if (mousePosition.y <= borderThickness)
        {
            moveDirection.z -= 1;
        }


        // Move the camera
        var position = cameraTargetTransform.position;
        position += moveDirection.normalized * (moveSpeed * Time.deltaTime);

        // Clamp the camera position to the limits
        position = new Vector3(
            Mathf.Clamp(position.x, bottomLeftLimit.x, topRightLimit.x),
            transform.position.y,
            Mathf.Clamp(position.z, bottomLeftLimit.y, topRightLimit.y)
        );

        cameraTargetTransform.position = position;
    }

    void MoveCameraTargetPositionIncrementaly(Vector2 mouseMovement)
    {
        Vector3 moveDirection = Vector3.zero;

        // Move the camera
        var position = cameraTargetTransform.position;
        position += moveDirection.normalized * (moveSpeed * Time.deltaTime);

        // Clamp the camera position to the limits
        position = new Vector3(
            Mathf.Clamp(position.x, bottomLeftLimit.x, topRightLimit.x),
            transform.position.y,
            Mathf.Clamp(position.z, bottomLeftLimit.y, topRightLimit.y)
        );

        cameraTargetTransform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("moving with gamePad");
        // If gamepad is connected, move the camera using the gamepad
        if (context.control.device is Gamepad)
        {
            isMouseMovementEnabled = false;
            Vector2 moveInput = context.ReadValue<Vector2>();
            MoveCameraTargetPositionIncrementaly(moveInput);
        }

        if (context.control.device is Mouse)
        {
            isMouseMovementEnabled = true;
        }
    }

    public void Enable(bool enable)
    {
        CMcamera.Priority = enable ? 10 : 0;
    }

    public void SetInputs(PlayerInputs playerInputs)
    {
        this.playerInputs = playerInputs;
    }
}