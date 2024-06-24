using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void Start()
    {
        SetDestination(GameObject.Find("EnemyTarget").transform.position);
        GetComponent<Health>().OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponentInChildren<Animator>().SetTrigger("Die");
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 10);
    }
}
