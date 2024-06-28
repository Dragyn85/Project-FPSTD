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
            GameObject myUIItemDraggedGameObject = eventData.pointerDrag.gameObject;
            UI_Item myUIItemComponentInDraggedUIItemGameObject = myUIItemDraggedGameObject.GetComponent<UI_Item>();
            UI_Slot myUISlotInjectionInDraggedUIItemGameObject = myUIItemComponentInDraggedUIItemGameObject.GetUISlot();
            
            //    .2- Landing:  UI-Slot
            //
            GameObject myUISlotGameObjectReceiverOfDrop = this.gameObject;
            UI_Slot myUISlotComponentInReceiverGameObject = myUISlotGameObjectReceiverOfDrop.GetComponent<UI_Slot>();
            UI_Item myUIItemInjectionInReceiverGameObject = myUISlotComponentInReceiverGameObject.GetUI_Item();

            // Validation:   Check if UI_SLOT:  Has a connection (dependency Injection) to:  UI_ITEM   (already).
            //
            if ( myUIItemInjectionInReceiverGameObject == null )
            {
                // UI_SLOT is empty:  No connection to:  UI_ITEM.
                // Let's add the UI_ITEM connection  INJECTION
                //
                // 1- Place the ITEM ("UI Element") correctly inside the "UI Slot-Element" 
                //
                myUIItemDraggedGameObject.GetComponent<RectTransform>().anchoredPosition = myUISlotComponentInReceiverGameObject.GetComponent<RectTransform>().anchoredPosition;

                // Update GUI Code Behind:  UI_Item, UI_Slot, UI_Inventory
            
                // 2- Update Connections:
                // Update: UI_Slot  (of previous:  UI_ITEM )  (not having the ITEM anymore)
                //
                myUIItemComponentInDraggedUIItemGameObject.RemoveUIItem( false );
            
                // Update: Landing: UI_Slot   (having a new connection to: the UI_ITEM)
                // ...rather: Dragged ITEM: update connections to new UI_SLot
                //
                myUIItemComponentInDraggedUIItemGameObject.AddUIItem( myUISlotComponentInReceiverGameObject );
                
            }//End if ( myUIItemInjectionInReceiverGameObject == null )
            else
            {
                Debug.LogWarning($"Error on --->OnDrop (cannot DROP UI_ITEM): UI_SLOT is NOT empty:  THERE'S a Previous Connection to a:  UI_ITEM.\n\n * The System will try to place the UI_ITEM in its original Position / UI_Slot.");
                
                
                // Try to place the UI_ITEM in its original Position / UI_Slot.
                //
                DragAndDrop uiSLotGameObjectDragAndDrop = myUIItemDraggedGameObject.GetComponent<DragAndDrop>();

                // Validate
                //
                if ( uiSLotGameObjectDragAndDrop != null )
                {
                    // Place the RectTransform of the UI GameObject in its original Position (x, y, z).
                    //
                    myUIItemDraggedGameObject.GetComponent<RectTransform>().anchoredPosition =
                        uiSLotGameObjectDragAndDrop.GetLast2DPositionOfUIElementBeforeDragNDrop();

                }//End if ( uiSLotGameObjectDragAndDrop != null )

            }//End else of if ( myUIItemInjectionInReceiverGameObject == null )
        }//End if (eventData.pointerDrag != null)
        else
        {
            Debug.LogWarning($"Error on --->OnDrop");

        }//End else of if (eventData.pointerDrag != null)
        
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
