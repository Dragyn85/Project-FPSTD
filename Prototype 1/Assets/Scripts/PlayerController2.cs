using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    public float runSpeed = 4.0f;
    public Rigidbody rb;


    public Transform cameraTransform;
    public float pitch;
    public float mouseSensitivity = 3.0f;

    public Transform gunPointTransform;
    public Bullet bulletPrefab;
    public float nextFireTime;

    public bool fpsMode = true;
    private PlayerInputs playerInputs;


    void Start()
    {
        playerInputs = new PlayerInputs();
        
        playerInputs.FPS.Enable();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(playerInputs.FPS.Move.ReadValue<Vector2>());
        HandleMouseLook(playerInputs.FPS.Look.ReadValue<Vector2>());
        HandleShooting();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fpsMode = !fpsMode;
            if (fpsMode)
            {
                playerInputs.FPS.Enable();
                playerInputs.GodMode.Disable();
            }
            else
            {
                playerInputs.GodMode.Enable();
                playerInputs.FPS.Disable();
            }

        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 0.5f;

            Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
        }
    }

    private void HandleMouseLook(Vector2 inputs)
    {
        var mouseX = inputs.x * mouseSensitivity;
        var mouseY = inputs.y * mouseSensitivity;


        transform.Rotate(Vector3.up, mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80, 80);

        var currentRot = cameraTransform.rotation.eulerAngles;
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    private void HandleMovement(Vector2 input)
    {
        var horizontal = input.x;
        var vertical = input.y;


        var inputMovement = (transform.forward * vertical + transform.right * horizontal)
                            * (runSpeed * (Input.GetKey(KeyCode.LeftShift) ? 2 : 1));
        var newVelocity = new Vector3(inputMovement.x, rb.linearVelocity.y, inputMovement.z);

        rb.linearVelocity = newVelocity;
    }
}