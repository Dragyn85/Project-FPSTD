using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Basic "Item" Object and Logic.
/// Todo: This could be reworked with Interfaces: Item : IStackable, IDraggable, etc..
/// Todo: This Class could be a Scriptable Object.
/// </summary>
[Serializable]
public class Item       // Todo: IStackable, IDraggable, etc..
{

    #region Attributes
    
    [Tooltip("[Readonly - for Debug]:  This ITEMS's ID in Database.")]
    [SerializeField]
    private int _itemID;
    
    [FormerlySerializedAs("itemType")]
    [Tooltip("[Readonly - for Debug]:  This ITEMS's 'ItemType'.")]
    [SerializeField]
    private ItemType _itemType;

    [FormerlySerializedAs("amount")]
    [Tooltip("[Readonly - for Debug]:  Total Amount of Items of this 'ItemType'.")]
    [SerializeField]
    private int _amount = 0;
    
    #endregion Attributes
    
    
    #region Custom Sub-Types
    
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
    }
    
    #endregion Custom Sub-Types

    

    #region Methods
    
    public Sprite GetSprite()
    {
        switch (_itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.Medkit:       return ItemAssets.Instance.medkitSprite;
        }
    }

    public Color GetColor()
    {
        switch (_itemType)
        {
            default:
            case ItemType.Sword:        return new Color(1, 1, 1);
            case ItemType.HealthPotion: return new Color(1, 0, 0);
            case ItemType.ManaPotion:   return new Color(0, 0, 1);
            case ItemType.Coin:         return new Color(1, 1, 0);
            case ItemType.Medkit:       return new Color(1, 0, 1);
        }
    }

    #region Interface Methods
    
    public bool IsStackable()
    {
        switch (_itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Medkit:
                return false;
        }
    }
    
    #endregion Interface Methods

    #region Getters, Setters

    public int GetItemID()
    {
        return this._itemID;
    }
    
    public void SetItemID(int itemID)
    {
        this._itemID = itemID;
    }

    public ItemType GetItemType()
    {
        return this._itemType;
    }
    
    public void SetItemType(ItemType itemType)
    {
        this._itemType = itemType;
    }
    
    public int GetAmount()
    {
        return this._amount;
    }
    
    public void SetAmount(int amount)
    {
        this._amount = amount;
    }
    
    #endregion Getters, Setters

    #endregion Methods
    
}//End Class
