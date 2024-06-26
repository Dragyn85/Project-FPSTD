using UnityEngine;

/// <summary>
/// GUI Code Behind: Item in GUI  (provisional name of GameObject:  "itemSlotTemplate_1 (clone), ..., etc")
/// </summary>
public class UI_Item : MonoBehaviour
{

    #region Attributes

    /// <summary>
    /// GUI UI_Slot that is connected to this UI_ITEM Component.
    /// </summary>
    private UI_Slot _uiSlot;
    
    /// <summary>
    /// Represents an unit of: ITEM Data-Logic
    /// </summary>
    private Item _item;

    #endregion Attributes
   
    
    #region Methods

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
