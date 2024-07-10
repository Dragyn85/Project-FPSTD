using UnityEngine;

internal interface IPlace
{
    Vector3 GetNewPlaceablePosition();
    bool ShouldTryPlace();
    bool ShouldCancelPlacement();
    bool ShouldRotate();
}