using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory System's GUI ( Code Behind the GUI ).
/// </summary>
public class UI_Inventory : MonoBehaviour
{

    #region Attributes
    
    /// <summary>
    /// Inventory Data-base Structure.
    /// </summary>
    [Tooltip("Inventory Data-base Structure.")]
    [SerializeField]
    private Inventory _inventory;
    
    /// <summary>
    /// Main GUI Container for the Slots.
    /// </summary>
    [Tooltip("Main GUI Container for the Slots.")]
    [SerializeField]
    private Transform _itemSlotGrid;

    /// <summary>
    /// Array of all Slots (_inventory) in GUI:   some of them are full with an ITEM... some of them are empty.
    /// </summary>
    [Tooltip("Array of all Slots (_inventory) in GUI:   some of them are full with an ITEM... some of them are empty.")]
    [SerializeField]
    private List<UI_Slot> _arrayOfSlot;
    
    /// <summary>
    /// Main GUI Container for the Items. This is the GUI ITEMS Parent GameObject.
    /// </summary>
    [Tooltip("Main GUI Container for the Items. This will be cloned for creating ITEMS.")]
    [SerializeField]
    private Transform _itemSlotContainer;
    
    /// <summary>
    /// A GUI ITEM. GUI Container for the Items. This will be cloned for creating ITEMS.
    /// </summary>
    [Tooltip("Main GUI Container for the Items. This will be cloned for creating ITEMS.")]
    [SerializeField]
    private Transform _itemSlotTemplate;

    /// <summary>
    /// Player in the Game.
    /// </summary>
    [Tooltip("Player in the Game.")]
    [SerializeField]
    private Player _player;

    #endregion Attributes
    
    
    #region Unity Methods

    private void Awake()
    {
        _itemSlotContainer = transform.Find("itemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("itemSlotTemplate");
    }

    #endregion Unity Methods
    
    
    #region Methods
    
    public void SetPlayer(Player player)
    {
        this._player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;

        // Subscribe to the Events (Observer Pattern): whenever the "Inventory Data" changes...:
        //
        // Previous one: inventory.OnItemListChanged += Inventory_OnItemListChanged;
        // TRIGGERED by a Database: "Add Item"
        //
        inventory.OnItemListChangedAddedItem += Inventory_OnItemListChangedAddedItem;
        //
        // TRIGGERED by a Database: "Remove Item"
        //
        inventory.OnItemListChangedRemovedItem += Inventory_OnItemListChangedRemovedItem;

        
        // Massively adding all ITEMS (from Inventory as DATABASE) -> to GUI (UI_ITEMs):
        //
        int arrayOfItemsLength = _inventory.GetItemList().Count;
        //
        for (int i = 0; i < arrayOfItemsLength; i++)
        {
            
            // Create new UI_Item from Item (Data):
            //
            UI_Item uiItem = CreateNewGameObjectUIItemsAndAddToGUI( _inventory.GetItemList()[ i ], _arrayOfSlot[ i ] );

            // Fill the inventory  list of slots with all data (Pointers):  ( _inventory.GetItemList() )
            //
            //RefreshUIInventoryUIItems(null, uiItem );
            //
            RefreshUIInventoryUIItemsAfterAddUIItem( uiItem );

        }//End For

        // Refresh the UI Rendering elements
        //
        RefreshUIRendering();

    }// End SetInventory()

    
    #region Delegates / Observer Pattern

    
    /// <summary>
    /// Gets the "UI_Item" associated to an "Item" with:    _itemID = itemID.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public UI_Item GetUIItemByID(int itemID)
    {
        
        int uiInventoryLength = _arrayOfSlot.Count;

        for (int i = 0; i < uiInventoryLength; i++)
        {

            if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            {
                
                if (_arrayOfSlot[i].GetUI_Item().GetItem().GetItemID() == itemID)
                {
                    // Found it
                    //
                    return _arrayOfSlot[i].GetUI_Item();
                }
                
            }//End if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            
        }//End For

        return null;

    }//End GetUIItemByID()

    
    /// <summary>
    /// Gets the First "UI_Item" associated to an "ItemType".
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public UI_Item GetFirstUIItem(Item.ItemType itemType)
    {
        
        int uiInventoryLength = _arrayOfSlot.Count;

        for (int i = 0; i < uiInventoryLength; i++)
        {

            if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            {
                
                if (_arrayOfSlot[i].GetUI_Item().GetItem() != null)
                {
                    
                    if (_arrayOfSlot[i].GetUI_Item().GetItem().GetItemType() == itemType)
                    {

                        // Found it
                        //
                        return _arrayOfSlot[i].GetUI_Item();

                    }//End if .. itemType
                }//End if (_arrayOfSlot[i].GetUI_Item().GetItem() != null)
                
            }//End if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            
        }//End For

        return null;

    }//End GetFirstUIItem()
    
    
    /// <summary>
    /// Gets the First EMPTY "UI_Slot" in the list.
    /// </summary>
    /// <returns></returns>
    public UI_Slot GetFirstEmptyUISlot()
    {
        
        int uiInventoryLength = _arrayOfSlot.Count;

        for (int i = 0; i < uiInventoryLength; i++)
        {

            if ( _arrayOfSlot[ i ].GetUI_Item() == null )
            {

                 // Found it
                 //
                 return _arrayOfSlot[i];

            }//End if ( _arrayOfSlot[ i ].GetUI_Item() == null )
            
        }//End For

        return null;

    }//End GetFirstEmptyUISlot()
    
    
    
    /// <summary>
    /// Event that is triggered whenever an ITEM id ADDED to the DATABASE.
    /// ...so the GUI caches it and updates the Visuals accordingly.
    /// </summary>
    /// <param name="itemAmountUpdateOnly">TRUE: it was an Update of the AMOUNT... | FALSE: It was a NEW "ADD ITEM" in a NEW place in the Inventory List (Database).</param>
    /// <param name="myItem"></param>
    private void Inventory_OnItemListChangedAddedItem(bool itemAmountUpdateOnly, Item myItem)
    {
        
        // 1- It could be an Existing Item inside the Inventory:  update AMOUNT
        //
        if (itemAmountUpdateOnly)
        {
            // DO nothing:  SKIP.
            
            // ACA_QUEDE  
            
            // Maybe: Update the TextMeshProNumbers for this Item on this UI_SLot.
            
        }//End if (addedTrueOrRemovedItemFalse)
        else
        {
            // 2- ...A NEW ITEM: update Amount
            // Added a new Item to a Database GUI SLOT: That TRIGGERED THIS Delegate Callback.
            //
            // Try to Add it (my UI_Item) to the GUI: to an EMPTY SLOT
            // Get First Empty UI_Slot
            //
            UI_Slot myEmptyUISlot = GetFirstEmptyUISlot();

            if (myEmptyUISlot != null)
            {

                // Create new UI_Item from Item (Data):
                //
                UI_Item uiItem = CreateNewGameObjectUIItemsAndAddToGUI(myItem, myEmptyUISlot);

                // Connect the newly added UI_ITEM to the rest of UI_SLOT and UI_Inventory
                //
                // RefreshUIInventoryUIItems(null, uiItem);
                //
                RefreshUIInventoryUIItemsAfterAddUIItem( uiItem );

            } //End if (myEmptyUISlot != null)
            else
            {
                Debug.LogError(
                    $"There's no EMPTY UI_SLOT in UI_INVENTORY... for ADDING one UI_ITEM.\n\nThis GameObject is:= {this.name}",
                    this);
            }

        }//Else of if (addedTrueOrRemovedItemFalse)

        // Refresh the UI Rendering elements
        //
        RefreshUIRendering();
        
    }// End Inventory_OnItemListChangedAddedItem

    
    /// <summary>
    /// Event that is triggered whenever an ITEM id is REMOVED from the DATABASE.
    /// ...so the GUI's "Code Behind" (i.e.: this script).. caches it and updates the Visuals accordingly.
    /// </summary>
    /// <param name="itemAmountUpdateOnly">TRUE: it was an Update of the AMOUNT... | FALSE: It was a real "REMOVE ITEM" (completely) from  place in the Inventory List (Database).</param>
    /// <param name="myItem"></param>
    private void Inventory_OnItemListChangedRemovedItem(bool itemAmountUpdateOnly, Item myItem)
    {
        
        // 1- It could be an Existing Item inside the Inventory:  update AMOUNT
        //
        if (itemAmountUpdateOnly)
        {
            
            // Skip.
            // Maybe: Update the TextMeshProNumbers for this Item AMOUNT on this UI_SLot.

        }//End if (itemAmountUpdateOnly)
        else
        {
            // Removed COMPLETELY the Item (from Database)  =>  REMOVE IT FROM GUI completely too.
            
            // Removed an Item from Database: That TRIGGERED THIS Delegate Callback.
            // Get the Item (it might not be there in UI...)
            //
            UI_Item myUIItem = GetUIItemByID( myItem.GetItemID() );
            
            
            // VALIDATION:   Removed "Item"  (it might not be there in UI...)
            //
            if (myUIItem == null)
            {
                
                Debug.LogWarning($"There's no UI_ITEM (that belongs to Database ITEM = { myItem }...\n...with that ID = { myItem.GetItemID() })...\n...to REMOVE it from ( GUI UI_SLOT ), UI_INVENTORY....\n\nThis GameObject is:= {this.name}", this);
                

                // The Item ID was a fake one (which means, it was deleted from the GUI,
                // ..by using  "UseAction()"  Delegate-callback).
                // Resolution of the Exception:   Search the "Item GUI" (UI_ITEM) BY TYPE...
                //...and remove THE FIRST ONE YOU FIND.
                //
                myUIItem = GetFirstUIItem( myItem.GetItemType() );

                
                // Super-Exception:
                // "myUIItem" is NULL too... so the GUI does NOT have that Item...
                // RESOLUTION:   DON'T DO ANYTHING, just complain with the Developers XD
                //
                if ( myUIItem == null )
                {
                    
                    Debug.LogError($"There's no UI_ITEM (that belongs to Database ITEM = { myItem }...\n...even when I do a SEARCH BY [temType = { myItem.GetItemType() })]...\n...to REMOVE it from ( GUI UI_SLOT ), UI_INVENTORY....\n-> NO FURTHER ACTIONS WILL BE PERFORMED.\n\nThis GameObject is:= {this.name}", this);

                    // Just End it HERE.

                }//End if ( myUIItem == null)
                
            }//End if (myUIItem == null)
            else
            {
                // It is:  myUIItem != null.
                // Everything is: CORRECT!
                
            }//End else of if (myUIItem == null)
            
                            
            // Try to REMOVE it (my UI_Item) from the GUI:
            //
            // There's an UI_SLOT => (Try to...) REMOVE its UI_ITEM  from the GUI:
            //
            RefreshUIInventoryUIItemsAfterRemovingUIItem( myUIItem.GetUISlot() );
                
            // Destroy the GameObject (that is the GUI Image/SpriteRenderer) from the Scene:
            //
            if (! TryDestroyGameObjectUIItemFromGUI( myUIItem ) )
            {
                Debug.LogError($"There's no GUI GameObject that has a UI_ITEM to DESTROY it ( GUI UI_ITEM ), UI_INVENTORY....\n\nThis GameObject is:= {this.name}", this);
            }
            
        }//End else of if (itemAmountUpdateOnly)

        // Refresh the UI Rendering elements
        //
        RefreshUIRendering();
        
    }// End Inventory_OnItemListChanged()
    

    #endregion Delegates / Observer Pattern

    #region Render GUI

    
    /// <summary>
    /// Refreshes the GUI ITEMS numbers and Images in the GUI of Inventory (based of the actual current state of the Invetory Database).
    /// </summary>
    private void RefreshUIRendering()
    {

        foreach (UI_Slot uiSlot in _arrayOfSlot)
        {

            // 1- Create new Item (as UI Element) on GUI.
            // Refresh: Add some Delegate Functions:  for "Mouse Input Actions" (Right CLick, Left Click, etc). 
            // ...and GUI
            //
            if (uiSlot.GetUI_Item() != null)
            {
                
                // Item Data:
                //
                Item item = uiSlot.GetUI_Item().GetItem();
                
                
                // For this "Item", on its Slot:
                // We refresh the ACTIONS (delegate functions) that the Item has,
                // ..for each possible Button: Mouse Right CLick, Left Click, etc.
                //
                uiSlot.GetUI_Item().GetComponent<Button_UI>().ClickFunc = () =>
                {
                    // Use item
                    _inventory.UseItem(item);
                    
                };
                uiSlot.GetUI_Item().GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    // Drop item
                    //Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                    //
                    Item duplicateItem = _inventory.CreateNewItem(item.GetItemType(), item.GetAmount());  
                    // new Item { itemType = item.itemType, amount = item.amount };
                    
                    _inventory.RemoveItem(item);
                    ItemWorld.DropItem(_player.GetPosition(), duplicateItem, false);
                    
                };

                // Set the TextMeshPro UI Text on the GUI:
                //
                TextMeshProUGUI uiText = uiSlot.GetUI_Item().GetComponent<RectTransform>().Find("amountText").GetComponent<TextMeshProUGUI>();
            
                // If the amount of items reaches one (1): don't show any number (or hide the TextMeshProUI uiText Object).
                //
                if (item.GetAmount() > 1)
                {
                    uiText.SetText(item.GetAmount().ToString());
                }
                else
                {
                    uiText.SetText("");
                } 
                
            }//End if (uiSlot.GetUI_Item() != null)
            else
            {
                // 2- There's no ITEMS in that UI_SLOT: empty
                //...Refresh colors of EMPTY UI_SLOTs
                //...REMOVE UI Elements (UI_ITEMS) that were in that Slot:
                // or Skip this:   if it was done already in another Method:  that's the case. OK!

            }//End else of if (uiSlot.GetUI_Item() != null)
            
        }//End For Each UI_Slot

    }// End RefreshUIRendering()


    /// <summary>
    /// DESTROYS a GameObject GUI in Scene with: ITEM as data, UI_ITEM as UI Element. 
    /// </summary>
    /// <param name="myUIItem"></param>
    /// <returns></returns>
    private bool TryDestroyGameObjectUIItemFromGUI(UI_Item myUIItem)
    {

        // 1- Search for: the Item (as UI Element) on GUI.
        //
        GameObject myGameObjectUIItem = myUIItem.gameObject;
        
        // Validate
        //
        if (myGameObjectUIItem != null)
        {
            // Destroy
            //
            Destroy( myGameObjectUIItem );

            return true;
        }
        else
        {
            // Failure:   Show some error message.
            //
            Debug.LogError($"UI_Inventory ERROR: Cannot DESTROY GameObject containing an UI_ITEM Component:  There's no such GameObject.\n\nThis Script is in: GameObject:= {this.name}", this);

            return false;

        }//End else of if (myGameObjectUIItem != null)
        
    }// End TryDestroyGameObjectUIItemFromGUI()


    
    /// <summary>
    /// Creates a new GameObject GUI in Scene with: item as data, UI_ITEM as UI Element.
    /// It also adds the Delegates for actions when the UI_ITEM is Clicked.
    /// NOTICE:  This method does NOT link the ITEM to the Database yet... neither it adds the UI_ITEM to any GUI UI_SLOT yet.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="uiSlot"></param>
    /// <returns></returns>
    private UI_Item CreateNewGameObjectUIItemsAndAddToGUI(Item item, UI_Slot uiSlot)
    {

        // 1- Create new Item (as UI Element) on GUI.
        //...then: Create (Instantiate) a set of GameObjects, which mean:  UI_SLOTS that are filled with those ITEMS.
        //...and: Add some Delegate Functions:  for "Mouse Input Actions" (Right CLick, Left Click, etc). 
        //
        RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
        //
        itemSlotRectTransform.gameObject.SetActive(true);
        //
        // Add a Component to the GameObject: UI_Item
        //
        bool couldGetComponentUIItem = itemSlotRectTransform.TryGetComponent<UI_Item>(out UI_Item uiItem);
        //
        if (! couldGetComponentUIItem )
        {

            // Create and Add Component if it was not there:
            //
            uiItem = itemSlotRectTransform.AddComponent<UI_Item>();    
        }
        //
        // Set UI_Item data: Item
        //
        uiItem.SetItem( item );
        //
        // Set UI_Slot
        //
        // Not here. This is done in the "RefreshUIInventoryUIItems()" Class:   uiSlot.SetUI_Item( uiItem );
        
        
        // For this "Item", on its Slot:
        // We refresh the ACTIONS (delegate functions) that the Item has,
        // ..for each possible Button: Mouse Right CLick, Left Click, etc.
        //
        itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            // Use item
            _inventory.UseItem(item);
        };
        itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
            // Drop item
            Item duplicateItem = _inventory.CreateNewItem(item.GetItemType(), item.GetAmount());  
                // new Item { itemType = item.itemType, amount = item.amount };

            _inventory.RemoveItem(item);
            ItemWorld.DropItem(_player.GetPosition(), duplicateItem , false );
        };

        itemSlotRectTransform.anchoredPosition = uiSlot.GetComponent<RectTransform>().anchoredPosition;
            // new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
        Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        image.sprite = item.GetSprite();
    
        TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
        
        // If the amount of items reaches one (1): don't show any number (or hide the TextMeshProUI uiText Object).
        //
        if (item.GetAmount() > 1)
        {
            uiText.SetText(item.GetAmount().ToString());
        }
        else
        {
            uiText.SetText("");
        }

        return uiItem;

    }// End CreateNewGameObjectUIItemsAndAddToGUI()

    
    
    /// <summary>
    /// Updated the GUI Code Behind..., whenever the "Data" changes in Inventory (because: Someone ADDED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    /// </summary>
    /// <param name="uiItem">UI_ITEM to be added to GUI</param>
    private void RefreshUIInventoryUIItemsAfterAddUIItem(UI_Item uiItem)
    {
        // There's an UI_ITEM => (Try to...) Add it to the GUI:
        //
        TryAddUIItemToGUI( uiItem );

    }// End RefreshUIInventoryUIItemsAfterAddUIItem()
    
    
    /// <summary>
    /// Updated the GUI Code Behind..., whenever the "Data" changes in Inventory (because: Someone REMOVED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    /// </summary>
    private void RefreshUIInventoryUIItemsAfterRemovingUIItem(UI_Slot uiSlot)
    {

        // UI_ITEM
        //
        UI_Item myUIItem = uiSlot.GetUI_Item();
        
        
        // There's an UI_SLOT => (Try to...) REMOVE its UI_ITEM  from the GUI:
        //
        TryRemoveUIItemGUIConnections( uiSlot );

    }// End RefreshUIInventoryUIItemsAfterRemovingUIItem()

    
    /// <summary>
    /// Updates this UI_ITEM connections to UI_SLOT (and viceversa), whenever the "Data" changes in Inventory (because: Someone ADDED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself:  triggering a Delegate-Callback). <br/><br/>
    /// </summary>
    /// <param name="uiItem">GUI Item Component in a GameObject</param>
    /// <returns>Success (true)... of the execution</returns>
    private bool TryAddUIItemToGUI(UI_Item uiItem)
    {
        // Empty UI_Slot, to add there the new UI_Item:
        //
        UI_Slot emptyUISlot = null;
        
        // 1- Search for the first empty Slot in the GUI:
        //
        for (int i = 0; i < _arrayOfSlot.Count; i++)
        {

            if (_arrayOfSlot[i].GetUI_Item() == null)
            {
                // Save the reference to the empty slot:
                //
                emptyUISlot = _arrayOfSlot[i];

                break;

            }//End if (_arrayOfSlot[i].GetUI_Item() != null)
            
        }//End For

        
        // 2- If "emptyUISlot" is really empty:   Add the new "UI_Item" there.
        //
        if ((emptyUISlot != null) && (emptyUISlot.GetUI_Item() == null))
        {
            
            // Connect the Item (Data) to <-> UI_Slot
            //
            uiItem.AddUIItem( emptyUISlot );

            return true;

        }//End if (emptyUISlot != null)
        else
        {
            // Failure:   Show some error message.
            //
            Debug.LogError($"UI_Inventory ERROR: Cannot ADD UI_ITEM to UI_SLOT:  There's no Empty UI_SLOTS in GUI (UI_Inventory: List<UI_Slot> _arrayOfSlot).\n\nThis GameObject is:= {this.name}", this);

            return false;

        }//End else of if (emptyUISlot != null)

    }// End TryAddUIItemToGUI()
    
    
    
    /// <summary>
    /// Updates the GUI Components Connections, whenever the "Data" changes in Inventory (because: Someone REMOVED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    ///
    /// * Removing ITEM: In this case the input parameter <code>UI_Item uiItem</code> is NULL. <br/>

    /// </summary>
    private bool TryRemoveUIItemGUIConnections(UI_Slot uiSlot)
    {
        
        // 1- Verify if the UI_Slot is really empty:
        //
        if (uiSlot != null)
        {
            
            // Check that the UI_Slot has an UI_Item
            //
            if (uiSlot.GetUI_Item() != null)
            {
                
                // 2- Remove the UI_Item from there.
                //
                UI_Item uiItem = uiSlot.GetUI_Item();
                //
                // .1- First remove the connection to UI_Slot in UI_Item + Remove the UI_Item from there.
                //
                uiItem.RemoveUIItem( false );
                
                return true;

            }//End if (uiSlot.GetUI_Item() != null)
            else
            {
                // The UI_Slot is EMPTY
                // Error
                //
                Debug.LogError($"UI_Inventory ERROR: Cannot REMOVE UI_ITEM from UI_SLOT:  The UI_SLOT does not have any UI_ITEMs... already  empty\n\n In GUI (UI_Inventory: List<UI_Slot> _arrayOfSlot).\n\nThis GameObject is:= {this.name}", this);
                
                return false;

            }//End if (uiSlot.GetUI_Item() != null)
            
        }//End if (uiSlot != null)
        
        Debug.LogError($"UI_Inventory ERROR: Cannot REMOVE UI_ITEM from UI_SLOT:  The UI_SLOT is a NULL\n\n In GUI (UI_Inventory: List<UI_Slot> _arrayOfSlot).\n\nThis GameObject is:= {this.name}", this);

        return false;
        
    }// End TryRemoveUIItemGUIConnections()

    

    #region Obsolete
    
    /// <summary>
    /// It Renders the GUI, whenever the "Data" changes in Inventory (because either: Someone ADDED or REMOVED an ITEM from the List Item,
    ///...which is the Inventory Data-Logic itself).
    /// </summary>
    [Obsolete("Use the version with input-parameters: RefreshUIInventoryUIItems(UI_Item uiItem) instead.")]
    private void RefreshInventoryItems()
    {

        // // Delete all the child-Objects of the "itemSlotContainer" (which is a RectTransform):
        // // ...that are not an:   "itemSlotTemplate"
        // //
        // foreach (Transform child in _itemSlotContainer)
        // {
        //     if (child == itemSlotTemplate) continue;
        //     
        //     Destroy(child.gameObject);
        // }
        //
        // int x = 0;
        // int y = 0;
        // float itemSlotCellSize = 75f;
        //
        // // 1- Render all Items (UI Elements) on GUI, together without empty Slots in the middle.
        // // For that: Cycle with a for-loop through every  Item in Data (Inventory):
        // //...then: Create (Instantiate) a set of GameObjects, which mean:  UI_SLOTS that are filled with those ITEMS.
        // //...and: Add some Delegate Functions:  for "Mouse Input Actions" (Right CLick, Left Click, etc). 
        // //
        // foreach (Item item in _inventory.GetItemList())
        // {
        //     RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        //     itemSlotRectTransform.gameObject.SetActive(true);
        //     
        //     // For every Item, on every Slot:
        //     // We refresh the ACTIONS (delegate functions) that the Item has,
        //     // ..for each possible Button: Mouse Right CLick, Left Click, etc.
        //     //
        //     itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
        //     {
        //         // Use item
        //         _inventory.UseItem(item);
        //     };
        //     itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        //     {
        //         // Drop item
        //         Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        //         _inventory.RemoveItem(item);
        //         ItemWorld.DropItem(_player.GetPosition(), duplicateItem);
        //     };
        //
        //     itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
        //     Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        //     image.sprite = item.GetSprite();
        //
        //     TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
        //     
        //     // If the amount of items reaches one (1): don't show any number (or hide the TextMeshProUI uiText Object).
        //     //
        //     if (item.amount > 1)
        //     {
        //         uiText.SetText(item.amount.ToString());
        //     }
        //     else
        //     {
        //         uiText.SetText("");
        //     }
        //
        //     x++;
        //     // Todo: Be careful with this Hard-code values:  Use attributes in the class instead
        //     // ...(these are the UI dimensions of the Inventory UI). Rows = 2 = y, Columns = 4 = x.
        //     //
        //     if (x >= 4)
        //     {
        //         x = 0;
        //         y++;
        //     }
        // }
    }// End RefreshUIInventoryUIItems()

    #endregion Obsolete
    
    
    #endregion Render GUI
    
    #endregion Methods
}
