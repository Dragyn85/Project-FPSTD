using UnityEngine;

/// <summary>
/// GUI Code Behind: "Slot" (of Inventory) in GUI  (provisional name of GameObject:  "ItemSlot, ..., etc")
/// ...Parent GamObject:  "ItemSlotGrid"
/// </summary>
public class UI_Slot : MonoBehaviour
{

    #region Attributes
    
    /// <summary>
    /// UI_Item Object, which is the GUI Visual representation of an UI_Item
    /// </summary>
    [Tooltip("UI_Item Object, which is the GUI Visual representation of an UI_Item")]
    [SerializeField]
    private UI_Item _myUI_Item;

    #endregion Attributes

    
    #region Unity Methods
    
    #endregion Unity Methods

    
    #region Methods
    
    public void SetUI_Item(UI_Item item)
    {
        _myUI_Item = item;
    }

    public UI_Item GetUI_Item()
    {
        return this._myUI_Item;
    }

    #endregion Methods

}
