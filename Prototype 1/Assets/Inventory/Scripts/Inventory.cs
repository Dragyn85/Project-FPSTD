using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the main class, that handles an Inventory System.
/// </summary>
public class Inventory 
{

    #region Attributes

    // Todo: Change this Temporary approach: for now this is a "Singleton".
    //
    public static Inventory Instance { get; private set; }

    /// <summary>
    /// Latest (Biggest) ITEMS's ID in Database added.
    /// </summary>
    private int _latestBiggestItemID = 0;

    /// <summary>
    /// Delegate Function: Observer Patter (this is the "Observed" or "Subject")... and this is the "Event" to be Fired
    /// ...whenever the List Items  (Data...) changes.
    ///
    /// Previous one:  public event EventHandler OnItemListChanged;
    /// Parameters:
    /// bool:  TRUE: JUST updated the item AMOUNT......  FALSE: Added/Removed completely an ITEM object.
    /// Item:  Item that was: REMOVED / ADDED
    /// </summary>
    public event Action<bool, Item> OnItemListChangedAddedItem;
    //
    public event Action<bool, Item> OnItemListChangedRemovedItem;

    /// <summary>
    /// This is the INVENTORY Database in itself.
    /// </summary>
    private List<Item> _itemList;
    
    private Action<Item> _useItemAction;
    
    #endregion Attributes
    
    
    #region Constructors
    
    public Inventory(Action<Item> useItemAction)
    {
        // Todo: Change this. Singleton approach.
        //
        Instance = this;
        
        this._useItemAction = useItemAction;
        
        // Create Database
        //
        _itemList = new List<Item>();

        // Adding some ITEMS to DATABASE:
        //
        AddItem( CreateNewItem( Item.ItemType.Sword, 1 ));
        AddItem( CreateNewItem( Item.ItemType.HealthPotion, 1 ));
        AddItem( CreateNewItem( Item.ItemType.ManaPotion, 1 ));

    }// End Inventory

    #endregion Constructors

    
    #region Methods
    
    /// <summary>
    /// Add a New Item to Database (only to the Logic Layer
    /// ...the GUI is not updated: for that we Trigger a Callback (a function Delegate (C# Action Event) announcing that the "Adding Item" process was executed)).
    /// </summary>
    /// <param name="item"></param>
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

            foreach (Item inventoryItem in _itemList)
            {

                // "Item" already in Inventory
                //
                if (inventoryItem.GetItemType() == item.GetItemType())
                {

                    // Add the Item as:  new Amount + previous Amount:
                    // New Item Amount:
                    //
                    int newItemAmount = item.GetAmount();
                    
                    // Total Amount: "New Item Amount" + "Previous Item Amount"
                    //
                    inventoryItem.SetAmount( inventoryItem.GetAmount() + newItemAmount );

                    itemAlreadyInInventory = true;
                    
                    // Trigger the Callback: Say that it is an AMOUNT UPDATE ("true") only:
                    //
                    OnItemListChangedAddedItem?.Invoke(true, item);
                    
                    break;
                }
            }
            if (!itemAlreadyInInventory)
            {

                // Add the newly create Item:

                // Create New "ID"  (for "Item")
                //
                item.SetItemID( GenerateNewItemID() );
                //
                // Add to List  (i.e.: Inventory - Database)
                //
                _itemList.Add(item);
                
                // Trigger the Callback: Say that it is NOT an AMOUNT UPDATE ("false")...
                //...but and New Addition to an Unique Inventory registry (i.e.: Empty Slot):
                //
                OnItemListChangedAddedItem?.Invoke(false, item);

            }//End if (!itemAlreadyInInventory)
        }
        else
        {
            // Not Stackable:
            
            // Create New ID  (for "Item")
            //
            item.SetItemID( GenerateNewItemID() );
            //
            // Not Stackable: It should be in a Unique Slot:
            //
            _itemList.Add(item);
            
            // Trigger the Callback: Say that it is NOT an AMOUNT UPDATE ("false")...
            //...but and "New Addition" to an Unique Inventory registry (i.e.: Empty Slot):
            //
            OnItemListChangedAddedItem?.Invoke(false, item);
            
        }//End if (item.IsStackable())
        
        // Previous: OnItemListChanged?.Invoke(this, EventArgs.Empty);
        //
    }// End AddItem()

    
    /// <summary>
    /// Removes one Unit of: Item from Database. It: decreases the AMOUNT of that Item in the Database. 
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        
        if (item.IsStackable())
        {
            
            // Look for the Amount (of the ITEM) in Database
            //
            Item itemInInventory = null;
            
            // Search for the ITEM  (by:  Item_Type: SWORD, HEALTH, COIN, ETC)
            //
            foreach (Item inventoryItem in _itemList)
            {
                
                if (inventoryItem.GetItemType() == item.GetItemType())
                {
                    
                    // Found it
                    // Decrease (UPDATE) the amount  (could reach lower than zero)
                    //
                    int itemAmount = item.GetAmount();
                    itemAmount--;
                    //
                    inventoryItem.SetAmount( itemAmount );
                    itemInInventory = inventoryItem;

                    break;
                }
            }
            if (itemInInventory != null)
            {

                if (itemInInventory.GetAmount() <= 0)
                {
                    
                    // Remove it
                    // AMOUNT Lower than zero: REMOVE IT from Database:
                    //
                    _itemList.Remove(itemInInventory);
                
                    // Remove it completely, not just updating Amount:  (so: false)
                    //
                    OnItemListChangedRemovedItem?.Invoke(false, item);
                    
                }//End if (itemInInventory.GetAmount() <= 0)
                else
                {
                    // Updating Amount: Don't REMOVE it completely:   There are still ITEMS there (so: true)
                    //
                    OnItemListChangedRemovedItem?.Invoke(true, item);
                    
                }//End else of if (itemInInventory.GetAmount() <= 0)

            }//End if (itemInInventory != null)
            else
            {
                // Failure:   Show some error message.
                //
                Debug.LogWarning($"Inventory ERROR: Cannot DESTROY GameObject containing an UI_ITEM Component:  There's no such GameObject.\n\nThis Script is in: GameObject:= {this}");
            }
        }
        else
        {
            // Not Stackable  (unique per Inventory Slot) => Just REMOVE IT.
            //
            _itemList.Remove(item);
            
            // Remove it completely, not just updating Amount:  (so: false)
            //
            OnItemListChangedRemovedItem?.Invoke(false, item);

        }//End else of if (item.IsStackable())
        
        // Previous:   OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }// End RemoveItem()

    
    public void UseItem(Item item)
    {
        _useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }

    public Item CreateNewItem(Item.ItemType myItemType, int myAmount)
    {

        // 1- Unique ID - Generate it:
        //
        int myItemID = GenerateNewItemID();
        
        Item myItem = new Item();
        //
        // Populate the ITEM with Data:
        //
        myItem.SetItemID( myItemID );
        myItem.SetItemType( myItemType );
        myItem.SetAmount( myAmount );

        return myItem;

    }// End CreateNewItem

    /// <summary>
    /// Generates a new Biggest number for "Item ID".
    /// </summary>
    /// <returns>Unique ID</returns>
    public int GenerateNewItemID()
    {
        // Increase "_latestBiggestItemID":
        //
        this._latestBiggestItemID++;

        return this._latestBiggestItemID;
    }
    
    #endregion Methods

}
