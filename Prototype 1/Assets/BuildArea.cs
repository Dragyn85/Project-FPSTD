using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuildArea : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public Bounds GetBuildArea()
    {
        return boxCollider.bounds;
    }
}
