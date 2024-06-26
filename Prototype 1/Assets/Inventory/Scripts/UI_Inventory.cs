using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory System's GUI ( Code Behind the GUI ).
/// 
/// Todo: Remove the hard-coded values.
/// </summary>
public class UI_Inventory : MonoBehaviour
{

    #region Attributes
    
    /// <summary>
    /// Inventory Data-base Structure
    /// </summary>
    private Inventory _inventory;
    
    /// <summary>
    /// Main GUI Container for the Slots.
    /// </summary>
    private Transform _itemSlotGrid;

    /// <summary>
    /// Array of all Slots (_inventory) in GUI:   some of them are full with an ITEM... some of them are empty.
    /// </summary>
    [Tooltip("Array of all Slots (_inventory) in GUI:   some of them are full with an ITEM... some of them are empty.")]
    [SerializeField]
    private List<UI_Slot> _arrayOfSlot;
    
    /// <summary>
    /// Main GUI Container for the Items. This will be cloned for creating ITEMS.
    /// </summary>
    private Transform itemSlotContainer;
    
    /// <summary>
    /// Main GUI Container for the Items. This will be cloned for creating ITEMS.
    /// </summary>
    private Transform itemSlotTemplate;

    /// <summary>
    /// Player in the Game.
    /// </summary>
    private Player _player;

    #endregion Attributes
    
    
    #region Unity Methods

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
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

        // Subscribe to the Event (Observer Pattern): whenever the "Inventory Data" changes...:
        //
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        
        // Massively adding all ITEMS (from Inventory as DATABASE) -> to GUI (UI_ITEMs):
        //
        int arrayOfItemsLength = _inventory.GetItemList().Count;
        //
        for (int i = 0; i < arrayOfItemsLength; i++)
        {
            
            // Create new UI_Item from Item (Data):
            //
            UI_Item uiItem = CreateNewGameObjectUIItemsAndAddToGUI( _inventory.GetItemList()[ i ], _arrayOfSlot[ i ] );

            // Fill the inventory  list of slots with all data:  ( _inventory.GetItemList() )
            //
            RefreshUIInventoryUIItems(null, uiItem );

        }//End For

        // Refresh the UI Rendering elements
        //
        RefreshUIRendering();

    }// End SetInventory()

    
    #region Delegates / Observer Pattern

    /// <summary>
    /// Gets the First "UI_Item" associated to an "Item".
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public UI_Item GetFirstUIItem(Item item)
    {
        
        int uiInventoryLength = _arrayOfSlot.Count;

        for (int i = 0; i < uiInventoryLength; i++)
        {

            if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            {
                
                if (_arrayOfSlot[i].GetUI_Item().GetItem() == item)
                {
                    // Found it
                    //
                    return _arrayOfSlot[i].GetUI_Item();
                }
                
            }//End if ( _arrayOfSlot[ i ].GetUI_Item() != null )
            
        }//End For

        return null;

    }//End GetFirstUIItem()
    
    
    /// <summary>
    /// Gets the First EMPTY "UI_Slot" in the list.
    /// </summary>
    /// <param name="item"></param>
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

    
    
    private void Inventory_OnItemListChanged(bool addedTrueOrRemovedItemFalse, Item myItem)
    {

        if (addedTrueOrRemovedItemFalse)
        {

            // Added an Item on Database: That TRIGGERED THIS Delegate Callback.
            //
            // Try to Add it (my UI_Item) to the GUI:
            // Get First Empty UI_Slot
            //
            UI_Slot myEmptyUISlot = GetFirstEmptyUISlot();

            if (myEmptyUISlot != null)
            {
                
                // Create new UI_Item from Item (Data):
                //
                UI_Item uiItem = CreateNewGameObjectUIItemsAndAddToGUI( myItem, myEmptyUISlot );

                // Connect the newly added UI_ITEM to the rest of UI_SLOT and UI_Inventory
                //
                RefreshUIInventoryUIItems(null, uiItem );
                
            }//End if (myEmptyUISlot != null)
            else
            {
                Debug.LogError($"There's no EMPTY UI_SLOT in UI_INVENTORY... for ADDING one UI_ITEM.\n\nThis GameObject is:= {this.name}", this);
            }

        }//End if (addedTrueOrRemovedItemFalse)
        else
        {
            // Removed the Item
            
            // Removed an Item from Database: That TRIGGERED THIS Delegate Callback.
            // Get the Item (it might not be there in UI...)
            //
            UI_Item myUIItem = GetFirstUIItem( myItem );
            
            //
            if (myUIItem != null)
            {

                // Try to REMOVE it (my UI_Item) from the GUI:
                //
                // There's an UI_SLOT => (Try to...) REMOVE its UI_ITEM  from the GUI:
                //
                TryRemoveUIItemFromGUI( myUIItem.GetUISlot() );
                
            }//End if (myUIItem != null)
            
        }//End if (addedTrueOrRemovedItemFalse)

        // Refresh the UI Rendering elements
        //
        RefreshUIRendering();
        
    }// End Inventory_OnItemListChanged()
    
    
    
    // private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    // {
    //     // TODO:  Correct this
    //     // 1- Delegate for: ADD ITEM to database
    //     // 2- Delegate for: REMOVE ITEM from database
    //     // RefreshUIInventoryUIItems(e.  UI_Slot uiSlot, UI_Item uiItem);
    //     
    //     
    // }

    #endregion Delegates / Observer Pattern

    #region Render GUI

    
    private void RefreshUIRendering()
    {

        foreach (Item item in _inventory.GetItemList()) //(UI_Slot uiSlot in _arrayOfSlot)
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
                    Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                    _inventory.RemoveItem(item);
                    ItemWorld.DropItem(_player.GetPosition(), duplicateItem);
                    
                };

                // Set the TextMeshPro UI Text on the GUI:
                //
                TextMeshProUGUI uiText = uiSlot.GetUI_Item().GetComponent<RectTransform>().Find("amountText").GetComponent<TextMeshProUGUI>();
            
                // If the amount of items reaches one (1): don't show any number (or hide the TextMeshProUI uiText Object).
                //
                if (item.amount > 1)
                {
                    uiText.SetText(item.amount.ToString());
                }
                else
                {
                    uiText.SetText("");
                } 
                
            }//End if (uiSlot.GetUI_Item() != null)
            
        }//End For Each UI_Slot

    }// End RefreshUIRendering()

    

    private UI_Item CreateNewGameObjectUIItemsAndAddToGUI(Item item, UI_Slot uiSlot)
    {

        // 1- Create new Item (as UI Element) on GUI.
        //...then: Create (Instantiate) a set of GameObjects, which mean:  UI_SLOTS that are filled with those ITEMS.
        //...and: Add some Delegate Functions:  for "Mouse Input Actions" (Right CLick, Left Click, etc). 
        //
        RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
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
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            _inventory.RemoveItem(item);
            ItemWorld.DropItem(_player.GetPosition(), duplicateItem);
        };

        itemSlotRectTransform.anchoredPosition = uiSlot.GetComponent<RectTransform>().anchoredPosition;   // new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
        Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        image.sprite = item.GetSprite();
    
        TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
        
        // If the amount of items reaches one (1): don't show any number (or hide the TextMeshProUI uiText Object).
        //
        if (item.amount > 1)
        {
            uiText.SetText(item.amount.ToString());
        }
        else
        {
            uiText.SetText("");
        }

        return uiItem;

    }// End CreateNewGameObjectUIItemsAndAddToGUI()

    
    /// <summary>
    /// It Renders the GUI, whenever the "Data" changes in Inventory (because: Someone either ADDED or REMOVED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    ///
    /// * Adding ITEM:  In this case the input parameter <code>I_Item uiItem</code> is NOT NULL. <br/>
    /// * Removing ITEM: In this case the input parameter <code>UI_Slot uiSlot</code> is NOT NULL. <br/>
    /// </summary>
    private void RefreshUIInventoryUIItems(UI_Slot uiSlot, UI_Item uiItem)
    {
        // 1- Determine the action to execute: ADD ITEM + Set it...or REMOVE IT.
        //
        if (uiItem != null)
        {
            // There's an UI_ITEM => (Try to...) Add it to the GUI:
            //
            TryAddUIItemToGUI( uiItem );
            
        }
        if (uiSlot != null)
        {

            // There's an UI_SLOT => (Try to...) REMOVE its UI_ITEM  from the GUI:
            //
            TryRemoveUIItemFromGUI( uiSlot );
            
            // Remove the Data   (Notice: this was triggered by GUI)
            //
            _inventory.RemoveItem(uiItem.GetItem());
        }

    }// End RefreshUIInventoryUIItems()

    
    /// <summary>
    /// It Renders the GUI, whenever the "Data" changes in Inventory (because: Someone ADDED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    ///
    /// * Adding ITEM:  In this case the input parameter <code>I_Item uiItem</code> is NOT NULL. <br/>
    /// </summary>
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
            
            uiItem.AddUIItem( emptyUISlot );
            
            // emptyUISlot.SetUI_Item( uiItem );
            //
            // uiItem.SetUISlot( emptyUISlot );

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
    /// It Renders the GUI, whenever the "Data" changes in Inventory (because: Someone REMOVED an ITEM from the <code> List Item </code>,
    ///...which is the Inventory Data-Logic itself). <br/><br/>
    ///
    /// * Removing ITEM: In this case the input parameter <code>I_Item uiItem</code> is NULL. <br/>

    /// </summary>
    private bool TryRemoveUIItemFromGUI(UI_Slot uiSlot)
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
                uiItem.RemoveUIItem( false );
                
                // //    .1- First remove the connection to UI_Slot in UI_Item:
                // //
                // uiSlot.GetUI_Item().SetUISlot(null);
                // //
                // //    .2- Remove the UI_Item from there.
                // //
                // uiSlot.SetUI_Item(null);

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
        
    }// End TryRemoveUIItemFromGUI()

    

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
        // foreach (Transform child in itemSlotContainer)
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
