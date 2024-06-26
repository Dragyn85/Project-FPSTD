/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDropOnSlot : MonoBehaviour, IDropHandler /*, IPointerEnterHandler, IPointerExitHandler */ 
{

    #region Attributes

    [Tooltip("...")]
    [SerializeField]
    private DragAndDrop _myDragAndDropScript;


    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>



    /// <summary>
    /// Update is called once per frame
    /// </summary>


    #endregion Unity Methods
    

    #region My Custom Methods


    #region Interfaces Methods

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"---> OnDrop");
    
        if (eventData.pointerDrag != null)
        {
            // 0- Define GameObjects:
            //    .1- ITEM
            //
            GameObject myUIItemDragged = eventData.pointerDrag.gameObject;
            UI_Slot myUIItemDraggedUISlot = myUIItemDragged.GetComponent<UI_Slot>();
            UI_Item myUIItemDraggedUIItem = myUIItemDragged.GetComponent<UI_Item>();
            
            //    .2- Landing:  UI-Slot
            //
            GameObject myUISlotLanding = this.gameObject;
            UI_Slot myUISlotLandingUISlot = myUISlotLanding.GetComponent<UI_Slot>();
            UI_Item myUISlotLandingUIItem = myUISlotLanding.GetComponent<UI_Item>();

            // 1- Place the ITEM ("UI Element") correctly inside the "UI Slot-Element" 
            //
            myUIItemDragged.GetComponent<RectTransform>().anchoredPosition = myUISlotLanding.GetComponent<RectTransform>().anchoredPosition;

            // Update GUI Code Behind:  UI_Item, UI_Slot, UI_Inventory
            
            // 2- Update Connections:
            // Update: UI_Slot  (of previous:  UI_ITEM )  (not having the ITEM anymore)
            //
            myUIItemDraggedUIItem.RemoveUIItem( false );
            
            // Update: Landing: UI_Slot   (having a new connection to: the UI_ITEM)
            // ...rather: Dragged ITEM: update connections to new UI_SLot
            //
            myUIItemDraggedUIItem.AddUIItem( myUISlotLandingUISlot );
        }
        else
        {
            Debug.LogWarning($"Error on --->OnDrop");
        }
        
    }// End OnDrop()

    
    #region other experiments
    
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log($"---> OnPointerEnter");
    //     
    //     if (eventData.pointerDrag != null)
    //     {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Error on OnDrop");
    //     }
    //     
    // }
    //
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     
    //     Debug.Log($"---> OnPointerExit");
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Debug.Log($"---> OnEndDrag");
    //
    //     if (eventData.pointerDrag != null)
    //     {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Error on OnDrop");
    //     }
    // }
    
    #endregion other experiments
    
    #endregion Interfaces Methods


    #endregion My Custom Methods

}
