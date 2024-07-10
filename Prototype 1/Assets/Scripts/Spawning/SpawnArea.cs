using UnityEngine;

public class SpawnArea : MonoBehaviour, ISpawnArea
{
    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }
}

public interface ISpawnArea
{
    Vector3 GetSpawnPosition();
}