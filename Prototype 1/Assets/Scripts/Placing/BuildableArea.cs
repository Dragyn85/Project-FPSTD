using UnityEngine;

public class BuildableArea : Validater
{
    Bounds[] PlacementAreas;

    private void Awake()
    {
        BuildArea[] buildAreas = FindObjectsOfType<BuildArea>();
        PlacementAreas = new Bounds[buildAreas.Length];

        for (int i = 0; i < buildAreas.Length; i++)
        {
            PlacementAreas[i] = buildAreas[i].GetBuildArea();
        }
        
    }

    public override bool IsValid()
    {
        for (var i = 0; i < PlacementAreas.Length; i++)
        {
            var placementArea = PlacementAreas[i];
            if (placementArea.Contains(transform.position))
            {
                return true;
            }
        }

        return false;
    }
}