using UnityEngine;

public class GodCameraController : MonoBehaviour
{
    public float moveSpeed = 20; // Speed at which the camera moves
    public float borderThickness = 10f; // Thickness of the screen border for triggering movement

    public Vector2 topRightLimit = new Vector2(100,100);
    public Vector2 bottomLeftLimit = new Vector2(-100,-100);
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
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
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        
        // Clamp the camera position to the limits
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bottomLeftLimit.y, topRightLimit.y)
        );
    }
}