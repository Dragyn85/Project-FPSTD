using System;
using com.davidhopetech.core.Run_Time.Extensions;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 0.4f) && rb.GetVelocity().y <= 0.1f)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
}
