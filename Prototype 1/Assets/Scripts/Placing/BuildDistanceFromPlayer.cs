using System;
using UnityEngine;

public class BuildDistanceFromPlayer : Validater
{
    [SerializeField] private float radiusFromPlayer = 10f;

    PlayerController playerController;
    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public override bool IsValid()
    {
        if(playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
            if (playerController == null)
            {
                return false;
            }
        }
        
        return Vector3.Distance(transform.position, playerController.transform.position) < radiusFromPlayer;
    }
}

public class BuildDistanceFromBase : Validater
{
    [SerializeField] private float radiusFromBase = 10f;
    private Transform baseTransform;

    private void Awake()
    {
        baseTransform = GameObject.Find("EnemyTarget").transform;
    }

    public override bool IsValid()
    {
        return Vector3.Distance(transform.position,baseTransform.position) < radiusFromBase;
    }
}