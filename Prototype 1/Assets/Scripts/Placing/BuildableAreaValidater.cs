using UnityEngine;

public class BuildableAreaValidater : Validater
{
    [SerializeField] BoxCollider PlacementArea;
    
    public override bool IsValid()
    {
        return PlacementArea.bounds.Contains(transform.position);
    }

}