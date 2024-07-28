using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 4.0f;
    public Rigidbody rb;

    public Transform cameraTransform;
    public float pitch;
    public float mouseSensitivity = 3.0f;

    public Transform gunPointTransform;
    public Bullet bulletPrefab;
    public GroundCheck groundCheck;
    public float nextFireTime;

    public bool fpsMode = true;
    private PlayerInputs playerInputs;

    [SerializeField] GodCameraController godCameraController;
    [SerializeField] float fireRate = 0.05f;
    
    Quaternion startRotation;
    [SerializeField] Transform gunTransform;

    private bool shouldJump;

    //[SerializeField] InputActionAsset InputAction;
    void Start()
    {
        playerInputs = new PlayerInputs();
        
        playerInputs.GodMode.SetCallbacks(godCameraController);

        startRotation = transform.localRotation;
        
        //godCameraController.SetInputs(playerInputs);
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
        HandleJump();
        HandleGunShake();
        //HandleGodMode();
    }

    private void HandleGunShake()
    {
        gunTransform.localRotation = Quaternion.Slerp(gunTransform.localRotation, startRotation, Time.deltaTime * 5);
    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            shouldJump = false;
        }
    }

    private void HandleJump()
    {
        if(playerInputs.FPS.Jump.triggered && groundCheck.IsGrounded)
        {
            shouldJump = true;
        }
    }

    private void HandleGodMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fpsMode = !fpsMode;
            if (fpsMode)
            {
                playerInputs.FPS.Enable();
                playerInputs.GodMode.Disable();
                godCameraController.Enable(false);

            }
            else
            {
                playerInputs.GodMode.Enable();
                playerInputs.FPS.Disable();
                godCameraController.Enable(true);
            }

        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
            float recoileForce = 3;
            AddRandomRecoil(recoileForce);
        }
    }

    private void AddRandomRecoil(float recoileForce)
    {
        var sideWayRecoil = recoileForce / 3;
        // Generate a random rotation amount within a range
        float randomXRotation = Random.Range(-recoileForce, 0);
        float randomZRotation = Random.Range(-sideWayRecoil, sideWayRecoil);

        // Create a rotation based on the random Euler angles
        Quaternion recoilRotation = Quaternion.Euler(randomXRotation, 0, randomZRotation);

        // Apply the recoil rotation to the gunTransform
        gunTransform.localRotation *= recoilRotation;
    }

    private void HandleMouseLook(Vector2 inputs)
    {
        var mouseX = inputs.x * mouseSensitivity;
        var mouseY = inputs.y * mouseSensitivity;


        //var newRotation = transform.rotation * Quaternion.Euler(0, mouseX, 0);
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