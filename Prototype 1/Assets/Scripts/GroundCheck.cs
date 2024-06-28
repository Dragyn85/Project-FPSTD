using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Collider playerCollider;
    [SerializeField] bool isGrounded;


    private Rigidbody rb;
    private Vector3 offset;
    private float slope;
    
    public float Slope => slope;
    public bool IsGrounded => isGrounded;

    public float distanceToGround;
    public float velocity;
    private void Awake()
    {
        var bounds = playerCollider.bounds;
        offset = new Vector3(0,-bounds.size.y/2,0);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position+offset, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,10f) && rb.linearVelocity.y <= 0.5f)
        {
            distanceToGround = hit.distance;
            slope = Vector3.Angle(hit.normal, transform.forward)-90;
            isGrounded = true;
            velocity = rb.linearVelocity.y;
        }
        else
        {
            isGrounded = false;
            velocity = rb.linearVelocity.y;
        }
    }
}
