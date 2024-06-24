using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class DesktopPlacementStartegy : IPlace
{
    private readonly Camera camera;
    
    public DesktopPlacementStartegy()
    {
        camera = Camera.main;
    }

    public Vector3 GetNewPlaceablePosition()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public bool ShouldTryPlace()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool ShouldCancelPlacement()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool ShouldRotate()
    {
        return Input.GetMouseButtonDown(2);
    }
}