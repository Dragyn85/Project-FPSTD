using UnityEngine;

/// <summary>
/// This is a Singleton class, that handles a reference to all the assets that we need in our scene's UI,
/// ...for the Inventory System to work.
/// </summary>
public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public Transform pfItemWorld;

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite medkitSprite;

}
