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
        
        Debug.LogWarning($"---> OnDrop");
    
        // Dragged GameObject ( eventData.pointerDrag.gameObject )
        //
        if (eventData.pointerDrag != null)
        {
            // 0- Define GameObjects:
            //    .1- Dragged:  ITEM
            //
            GameObject myUIItemDraggedGameObject = eventData.pointerDrag.gameObject;
            UI_Item myUIItemComponentInDraggedUIItemGameObject = myUIItemDraggedGameObject.GetComponent<UI_Item>();
            UI_Slot myUISlotInjectionInDraggedUIItemGameObject = myUIItemComponentInDraggedUIItemGameObject.GetUISlot();
            
            //    .2- Landing:  UI-Slot
            //
            GameObject myUISlotGameObjectReceiverOfDrop = this.gameObject;
            UI_Slot myUISlotComponentInReceiverGameObject = myUISlotGameObjectReceiverOfDrop.GetComponent<UI_Slot>();
            UI_Item myUIItemInjectionInReceiverGameObject; // = myUISlotComponentInReceiverGameObject.GetUI_Item();


            // Validation:   UI_Slot (Component) must be attached to the "Receiver" GameObject...
            //..or if it is not: this means that it is a "blank space" on the screen: we want to revert the UI_ITEM back to its original place if that happens: 
            //
            if (myUISlotComponentInReceiverGameObject != null)
            {
                // There's a  UI_Slot (Component)
                //
                myUIItemInjectionInReceiverGameObject = myUISlotComponentInReceiverGameObject.GetUI_Item();
                
                // Validation:   Check if UI_SLOT:  Has a connection (dependency Injection) to:  UI_ITEM   (already).
                //
                if ( myUIItemInjectionInReceiverGameObject == null )
                {
                    // UI_SLOT is empty:  No connection to:  UI_ITEM.
                    // UI_SLOT:  There' NO previous connection to:  UI_ITEM   (already).   (dependency Injection)
                    // That's OK. We are validated..: we can Drop the ITEM in this "empty SLOT".
                    
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
                    // UI_SLOT is invalid:  There' a previous connection to:  UI_ITEM   (already).   (dependency Injection)
                    // We can't DROP the ITEM in this "OCCUPIED SLOT".
                    
                    // Try to place the UI_ITEM in its original Position / UI_Slot.
                    //
                    WrapperForTryToPlaceDraggedGameObjectInThisLocationRectTransform(myUIItemDraggedGameObject);

                }//End else of if ( myUIItemInjectionInReceiverGameObject == null )

            }//End if (myUISlotComponentInReceiverGameObject != null)
            else
            {
                // There's no  UI_Slot (Component)  on the RECEIVER GameObject
                // It means: this is just a "Blank space":  revert the ITEM back to its position / place ( UI_SLOT ).
                //
                // Try to place the UI_ITEM in its original Position / UI_Slot.
                //
                WrapperForTryToPlaceDraggedGameObjectInThisLocationRectTransform(myUIItemDraggedGameObject);
                
            }//End else of if (myUISlotComponentInReceiverGameObject != null)

        }//End if (eventData.pointerDrag != null)
        else
        {
            Debug.LogWarning($"Error on --->OnDrop.\n Reason:  The Dragged GameObject is NULL\n\nThis GameObject is:= {this.name}", this);

        }//End else of if (eventData.pointerDrag != null)
        
    }// End OnDrop()

    
    /// <summary>
    /// Wrapper with Validations and Debug.LOG messages for:  <br /><br />
    /// Trying to place the UI_ITEM in its original Position / UI_Slot. <br /><br />
    /// 
    /// It should be used after a validation that detected and error on the "On Drop" process. 
    /// </summary>
    /// <param name="myUIItemDraggedGameObject">Dragged GameObject</param>
    /// <returns></returns>
    private void WrapperForTryToPlaceDraggedGameObjectInThisLocationRectTransform(GameObject  myUIItemDraggedGameObject)
    {
        // Try to place the UI_ITEM in its original Position / UI_Slot.
        //
        if (! TryToPlaceDraggedGameObjectInThisLocationRectTransform(myUIItemDraggedGameObject) )
        {
            Debug.LogError($"Error on --->OnDrop (cannot place the UI_ITEM in its original Position - revert back the process) + and + UI_SLOT is NOT empty:  THERE'S a Previous Connection to a:  UI_ITEM.\n\n * The System FAILED to: place the UI_ITEM in its original Position / UI_Slot.\n * Exceptional state of the UI_ITEM when DRAGGING AND DROPPING.\n\nThis GameObject is:= {this.name}", this);
                        
        }//End if (TryToPlaceDraggedGameObjectInThisLocationRectTransform(myUIItemDraggedGameObject))
        else
        {
            // We did RECOVER from a Exception:   We placed the UI_ITEM back to its previous Place (UI_SLOT)
                        
            // Previous Exception:  UI_SLOT was invalid:  There's a previous connection to:  UI_ITEM   (already).   (dependency Injection)
            //...We couldn't DROP the ITEM in this "OCCUPIED SLOT".
            //
            Debug.LogWarning($"Could RECOVER FROM: Soft Error on --->OnDrop (cannot DROP UI_ITEM): UI_SLOT is NOT empty:  THERE'S a Previous Connection to a:  UI_ITEM.\n\n * The System will try to place the UI_ITEM in its original Position / UI_Slot.\n\nThis GameObject is:= {this.name}", this);

        }//End else of if (TryToPlaceDraggedGameObjectInThisLocationRectTransform(myUIItemDraggedGameObject))
        
    }// End WrapperForTryToPlaceDraggedGameObjectInThisLocationRectTransform()
    
    
    /// <summary>
    /// Tries to place the UI_ITEM in its original Position / UI_Slot. <br /><br />
    /// 
    /// It should be used after a validation that detected and error on the "On Drop" process. 
    /// </summary>
    /// <param name="myUIItemDraggedGameObject">Dragged GameObject</param>
    /// <returns></returns>
    private bool TryToPlaceDraggedGameObjectInThisLocationRectTransform(GameObject  myUIItemDraggedGameObject)
    {
        // return value: success or failure
        //
        bool couldPlaceTheUIItemSuccessfully = false;
        
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

            couldPlaceTheUIItemSuccessfully = true;
            
        }//End if ( uiSLotGameObjectDragAndDrop != null )
        
        return couldPlaceTheUIItemSuccessfully;

    }// End TryToPlaceDraggedGameObjectInThisLocationRectTransform()
    

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
