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

    private void Awake()
    {
        var bounds = playerCollider.bounds;
        offset = new Vector3(0,-bounds.size.y/2,0);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position+offset, Vector3.down);
        if (Physics.Raycast(ray,out var hit,0.1f) && rb.linearVelocity.y <= 0.1f)
        {
            slope = Vector3.Angle(hit.normal, transform.forward)-90;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
