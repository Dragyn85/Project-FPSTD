using UnityEngine;
using UnityEngine.Serialization;

public class ItemWorldSpawner : MonoBehaviour
{

    [Tooltip("The ITEM data, to build an ITEM on GUI from.")]
    [FormerlySerializedAs("item")]
    [SerializeField]
    private Item _item;

    private void Awake()
    {
        ItemWorld.SpawnItemWorld(transform.position, _item);
        Destroy(gameObject);
    }

}
