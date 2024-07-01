using UnityEngine;

/// <summary>
/// GUI Code Behind: Item in GUI  (provisional name of GameObject:  "itemSlotTemplate(Clone)", ..., etc).
/// </summary>
public class UI_Item : MonoBehaviour
{

    #region Attributes

    /// <summary>
    /// GUI UI_Slot that is connected to this UI_ITEM Component.
    /// </summary>
    [Tooltip("GUI UI_Slot that is connected to this UI_ITEM Component.")]
    [SerializeField]
    private UI_Slot _uiSlot;
    
    /// <summary>
    /// Represents an unit of: ITEM Data-Logic.
    /// </summary>
    [Tooltip("Represents an unit of: ITEM Data-Logic.")]
    [SerializeField]
    private Item _item;

    #endregion Attributes
   
    
    #region Methods

    /// <summary>
    /// Links THIS (Component's references.. Dependency Injection):
    /// UI_ITEM (Component) to a UI_Slot (Component)... given "Item" Data (fully initialized previously) as Input.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="uiSlot"></param>
    public void AddUIItem(Item item, UI_Slot uiSlot)
    {
        this._item = item;
        //
        // Connections to UI_SLot
        //   1- From UI_Item
        //
        this._uiSlot = uiSlot;
            
        //   2 From UI_Slot
        //
        uiSlot.SetUI_Item( this );
    }
    
    /// <summary>
    /// Links THIS (Component's references.. Dependency Injection):
    /// UI_ITEM (Component) to a UI_Slot (Component).
    /// </summary>
    /// <param name="uiSlot"></param>
    public void AddUIItem(UI_Slot uiSlot)
    {
        
        // Connections to UI_SLot
        //   1- From UI_Item
        //
        this._uiSlot = uiSlot;
            
        //   2 From UI_Slot
        //
        uiSlot.SetUI_Item( this );
    }
    
    /// <summary>
    /// Removes THIS (Component's references.. Dependency Injection):
    /// UI_ITEM (Component) from its:  UI_Slot (Component)... leaving it completely disconnected from the GUI of INventory's GameObjects.
    /// </summary>
    /// <param name="disconnectFromItemDatabaseToo"></param>
    public void RemoveUIItem(bool disconnectFromItemDatabaseToo)
    {
        
        if (disconnectFromItemDatabaseToo)
        {
            this._item = null;

        } 
        // Get UI_SLot
        //
        UI_Slot uiSlot = this.GetUISlot();

        // Connections to UI_SLot
        //   1- From UI_Item
        //
        this._uiSlot = null;
            
        //   2 From the EXTERNAL "UI_Slot"
        //
        uiSlot.SetUI_Item( null );
    }
    
    public void SetItem(Item item)
    {
        this._item = item;
    }

    public Item GetItem()
    {
        return this._item;
    }

    public void SetUISlot(UI_Slot uiSlot)
    {
        this._uiSlot = uiSlot;
    }

    public UI_Slot GetUISlot()
    {
        return this._uiSlot;
    }
    
    #endregion Methods
    
}
