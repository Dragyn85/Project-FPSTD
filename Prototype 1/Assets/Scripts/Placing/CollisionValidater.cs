using UnityEngine;

public class CollisionValidater : Validater
{
    [SerializeField] BoxCollider boxCollider;

    public override bool IsValid()
    {
        var colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.size / 2,
            boxCollider.transform.rotation, LayerMask.GetMask("Placeable"));
        if (colliders.Length > 1)
        {
            return false;
        }

        return true;
    }
}