using UnityEngine;
//
//using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Rendering.Universal;
//
using TMPro;
using CodeMonkey.Utils;

/// <summary>
/// Logic for handling the Items' "Visual behaviour" (UI feedback mostly), and setting in a Scene (level).
/// </summary>
public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    /// <summary>
    /// Throws an Item from a given position (maybe the Player's Vector3 position).
    /// </summary>
    /// <param name="dropPosition"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 8f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);
        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D lightForUI;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightForUI = transform.Find("Light").GetComponent<Light2D>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        lightForUI.color = item.GetColor();
        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
