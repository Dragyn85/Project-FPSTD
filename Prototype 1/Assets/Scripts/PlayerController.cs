using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 4.0f;
    public Rigidbody rb;
    
    
    public Transform cameraTransform;
    public float pitch;
    public float mouseSensitivity = 3.0f;
    
    public Transform gunPointTransform;
    public Bullet bulletPrefab;
    public float nextFireTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if(Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 0.5f;
           
            Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
        }
    }

    private void HandleMouseLook()
    {
        var mouseX = Input.GetAxis("Mouse X")*mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        
        //var newRotation = transform.rotation * Quaternion.Euler(0, mouseX, 0);
        transform.Rotate(Vector3.up, mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80, 80);
        
        var currentRot = cameraTransform.rotation.eulerAngles;
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        
        var inputMovement = (transform.forward*vertical + transform.right*horizontal) 
                              * (runSpeed * (Input.GetKey(KeyCode.LeftShift) ? 2 : 1));
        var newVelocity = new Vector3(inputMovement.x, rb.linearVelocity.y, inputMovement.z);

        rb.linearVelocity = newVelocity;
    }
}