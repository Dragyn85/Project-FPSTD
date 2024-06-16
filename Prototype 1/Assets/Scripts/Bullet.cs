using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float damage = 10;
    
    private void Awake()
    {
        rb.AddForce(transform.forward * 100, ForceMode.Impulse);
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