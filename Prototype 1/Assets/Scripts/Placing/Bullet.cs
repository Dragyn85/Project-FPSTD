using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float Force => launchForce;
    
    [SerializeField] Rigidbody rb;
    [SerializeField] private float damage = 10;
    [FormerlySerializedAs("speed")] [SerializeField] private float launchForce = 10;
    private void Awake()
    {
        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        Destroy(gameObject,2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}