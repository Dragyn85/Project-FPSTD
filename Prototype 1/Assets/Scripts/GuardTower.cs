using System.Linq;
using UnityEngine;

public class GuardTower : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;

    float nextAttackTime = 0;

    Transform playerBase;
    Transform target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPointTransform;

    private void Start()
    {
        playerBase = GameObject.Find("EnemyTarget").transform;
        InvokeRepeating(nameof(UpdateTargets), 0.1f, 0.1f);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }


        // Face the target
        transform.LookAt(target.position);

        if (CanAttack())
        {
            Shoot();
        }
        else if (nextAttackTime < Time.time)
        {
            target = null;
        }
    }

    private bool CanAttack()
    {
        if (target == null)
        {
            return false;
        }

        if (Vector3.Distance(transform.position, target.position) < radius && Time.time > nextAttackTime)
        {
            return true;
        }

        if (Physics.Raycast(gunPointTransform.position, (target.position - gunPointTransform.position).normalized,
                out RaycastHit hit, radius))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }

        return false;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
        nextAttackTime = Time.time + 0.5f;
    }

    private void UpdateTargets()
    {
        if (target != null)
        {
            return;
        }

        Physics.SyncTransforms();
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (colliders.Length > 0)
        {
            colliders = colliders.OrderBy(t => Vector3.Distance(t.transform.position, playerBase.position)).ToArray();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<IDamagable>(out var damagable))
                {
                    target = collider.transform;
                    break;
                }
            }
        }
    }
}