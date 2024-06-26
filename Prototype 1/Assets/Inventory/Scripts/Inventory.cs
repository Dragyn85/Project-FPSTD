using System;
using System.Collections.Generic;

/// <summary>
/// This is the main class, that handles an Inventory System.
/// </summary>
public class Inventory 
{

    // Todo: Change this Temporary approach: for now this is a "Singleton".
    //
    public static Inventory Instance { get; private set; }

    /// <summary>
    /// Delegate Function: Observer Patter (this is the "Observed" or "Subject")... adn this is the "Event" to be Fired
    /// ...whenever the List Items  (Data...) changes.
    ///
    /// Previous one:  public event EventHandler OnItemListChanged;
    /// Parameters:
    /// bool:  TRUE: ADD......  FALSE: REMOVE from GUI.
    /// Item:  Item that was: REMOVED / ADDED
    /// </summary>
    public event Action<bool, Item> OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        // Todo: Change this. Singleton approach.
        //
        Instance = this;
        
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
    }

    public void AddItem(Item item)
    {
        // Todo note: This could be reworked with Interfaces: Item : IStackable, IDraggable, etc..
        //...in that case: this would be a Switch case:  SWITCH TYPE (OR SUB-TYPE, INTERFACE, ETC) MATCHING and SWITCH PATTERN (...WHEN A > 5) MATCHING
        //
        // Note 2: Each ITEM could be a "Scriptable Object": extending some Interface: IStackable, IMovable, IConsumable, etc.
        //
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
                
                OnItemListChanged?.Invoke(true, item);
            }
        }
        else
        {
            itemList.Add(item);
            
            OnItemListChanged?.Invoke(true, item);
        }
        // Previous: OnItemListChanged?.Invoke(this, EventArgs.Empty);
        //
        
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                
                itemList.Remove(itemInInventory);
                
                OnItemListChanged?.Invoke(false, item);
            }
        }
        else
        {
            itemList.Remove(item);
            
            OnItemListChanged?.Invoke(false, item);
        }
        // Previous:   OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

}
