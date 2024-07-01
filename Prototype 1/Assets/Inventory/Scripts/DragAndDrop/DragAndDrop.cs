/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
{

    #region Attributes

    #region CONSTANTS

    //  CONSTANTS
    private float _CANVAS_GROUP_ALPHA_SEMITRANSPARENT = 0.333f;
    
    #endregion CONSTANTS
    
    // Variables
    
    [Tooltip("GUI Canvas for extracting information: the UI Canvas Scale.")]
    [SerializeField]
    private Canvas _canvas;    
    
    [Tooltip("Rect Transform of the UI Visual Element we want to move around the screen (by Drag And Dropping it...).")]
    [SerializeField]
    private RectTransform _rectTransform;
   
    [Tooltip("Rect Transform of the Main Parent GameObject of all GameObjects containing UI_ITEM Component.")]
    [SerializeField]
    private RectTransform _rectTransformOfParentGameObject;
    
    [Tooltip("GUI Canvas Group for this (GUI) GameObject (that is being 'Dragged'): for setting its 'ALPHA COLOR' value... (or even blocking Raycasts (i.e.: Mouse Pointer interaction, to this GUI momentarily), but it will not be necessary because we are doing that on another 'Canvas Group' that belongs to its Parent GameObject.")]
    [SerializeField]
    private CanvasGroup _canvasGroupOnThisGameObject;

    
    [Tooltip("GUI Canvas Group for Parent GameObject of all GameObjects containing UI_ITEM Component: for blocking Raycasts (i.e.: Mouse Pointer interaction) to all 'UI_ITEMS' Item momentarily.")]
    [SerializeField]
    private CanvasGroup _canvasGroupOfParentGameObject;

    [Tooltip("[READONLY: For Debug Purposes] Last 2D Position of the Item (Rect Transform), before the 'Drag And Drop' process starts.")]
    [SerializeField]
    private Vector2 _last2DPositionOfUIElementBeforeDragNDrop = new Vector2(0, 0);

        
    #region OnDrop: Validations: Not being Droppped outside the GUI of Inventory
    
    [Tooltip("This is the Main 'RectTransform' that represents the GUI of Inventory (with all the Inventory SLOTS on GUI).\n\n* This property is good enough for Validating a possible 'OnDrop' outside the GUI of Inventory")]
    [SerializeField]
    private RectTransform _rectTransformOfInventoryGUI;
    
    #endregion OnDrop: Validations: Not being Droppped outside the GUI of Inventory

    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>
    private void Awake()
    {
        
        #region Ini RectTransforms
        
        // 1-
        //   .1- Get the 'RectTransform' of:  UI Element to Drag and Drop.
        //
        _rectTransform = GetComponent<RectTransform>();

        if (_rectTransform == null)
        {
        
            Debug.LogError($"There's no RectTransform in this GameObject... for DRAG AND DROP.\n\nThis GameObject is:= {this.name}", this);

        }//End if (_rectTransform == null)
        

        //   .2- Get the Main Parent 'RectTransform' of:  Parent GameObject of this ITEM, (UI Element to Drag and Drop).
        //
        if (_rectTransformOfParentGameObject == null)
        {
            
            // Try to GetComponent
            //
            _rectTransformOfParentGameObject = _rectTransform.parent.GetComponent<RectTransform>();

        
            if (_rectTransformOfParentGameObject == null)
            {
        
                Debug.LogError($"(Designer error) There's no link to a: RectTransform, from the PARENT GAMEOBJECT of this GameObject... for DRAG AND DROP.\n\nThis GameObject is:= {this.name}", this);
                
                // Add it:
                //
                _rectTransformOfParentGameObject = _rectTransform.parent.transform.gameObject.GetComponent<RectTransform>();

            }//End if (_rectTransform == null)

        }//End if (_rectTransform == null)
        
        
        // 2- Get the 'RectTransform' of:  Inventory GUI space (UI Element to Drop ITEMS on).
        // This will be used for Validations:   Drop (Item) Out of Bounds.
        //
        if (_rectTransformOfInventoryGUI == null)
        {
        
            Debug.LogError($"There's no 'RectTransform' of:  Inventory GUI space (UI Element to Drop ITEMS on) in this GameObject... for VALIDATING DRAG AND DROP (Drop (Item) Out of Bounds).\n\nThis GameObject is:= {this.name}", this);

        }//End if (_rectTransform == null)
        
        #endregion Ini RectTransforms
        

        #region Ini Canvas and CanvasGroups
        
        // 3- Add one CanvasGroup to THIS GUI GameObject?  this GUI ITEM?
        //
        if (_canvasGroupOnThisGameObject == null)
        {
            
            Debug.LogWarning($"There's no CanvasGroup on this particular ITEM (GUI GameObject)... for playing the ALPHA COLOR and maybe: blocking Raycasts (i.e.: Mouse Pointer interaction) to all GUI momentarily.\n\nThis GameObject is:= {this.name}... Adding one", this);

            
            // Get the Canvas Group Component or Add one.
            //
            CanvasGroup myOwnAuxCanvasGroup = GetComponent<CanvasGroup>();

            // Try to add Component
            //
            if (myOwnAuxCanvasGroup == null)
            {
                // Add Component to this  GameObject
                //
                _canvasGroupOnThisGameObject = gameObject.AddComponent<CanvasGroup>();

            }//End if (myOwnAuxCanvasGroup == null)
            else
            {
                // Add the previous Component to a reference
                //
                _canvasGroupOnThisGameObject = myOwnAuxCanvasGroup;

            }//End else of if (myOwnAuxCanvasGroup == null)
            
        }//End if (_canvasGroupOnThisGameObject == null)...
        
        
        // 4- Parent GameObject:  Get the Canvas Group Component or Add one.
        //
        // Add one CanvasGroup ?  (to Parent GameObject that Groups all these ITEMS together)
        //
        if (_canvasGroupOfParentGameObject == null)
        {
            
            // Try to get <CanvasGroup> Component from this Parent's GameObject:
            //
            _canvasGroupOfParentGameObject = _rectTransformOfParentGameObject.GetComponent<CanvasGroup>();

            if (_canvasGroupOfParentGameObject == null)
            {
                
                Debug.LogWarning($"There's no CanvasGroup in this GameObject's PARENT (GameObject)... for disabling Raycasts in 'DRAG AND DROP'.\n\nThis GameObject is:= {this.name}... Adding one", this);
            
                // Add Component to Parent (GameObject):
                //
                _canvasGroupOfParentGameObject = _rectTransformOfParentGameObject.gameObject.AddComponent<CanvasGroup>();
            }
            else
            {
                // Everything is OK.
            }//End else of if (_canvasGroupOfParentGameObject == null)
        
        }//End if (_canvasGroupOfParentGameObject == null)

        // DO we have a Main Canvas on a MAin GUI GameObject??
        //
        if (_canvas == null)
        {
            
            Debug.LogWarning($"There's no Main Canvas in this GameObject's PARENT (GameObject)... for disabling Raycasts in 'DRAG AND DROP' and validating it.\n\nThis GameObject is:= {this.name}... Adding one", this);
            
            // Add Component to Parent (GameObject):
            //
            _canvas = gameObject.transform.parent.gameObject.AddComponent<Canvas>();
            
        }//End if (_canvas != null)
        else
        {
        }//End if (_canvas == null)

        #endregion Ini Canvas and CanvasGroups
        
    }//End Awake


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    

    /// <summary>
    /// Update is called once per frame
    /// </summary>


    #endregion Unity Methods


    #region My Custom Methods

    #region Getters and Setters

    public Vector2 GetLast2DPositionOfUIElementBeforeDragNDrop()
    {
        return this._last2DPositionOfUIElementBeforeDragNDrop;
    }

    #endregion Getters and Setters

        
    #region Interfaces Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag");

        // Make it Semi-Transparent:
        //
        _canvasGroupOnThisGameObject.alpha = _CANVAS_GROUP_ALPHA_SEMITRANSPARENT;

        // Block Interaction with ALL UI Elements on this GUI CANVAS  (Items children of this Canvas Group)
        //
        _canvasGroupOfParentGameObject.blocksRaycasts = false;
    }

    
    /// <summary>
    /// Handles the actual 'Drag and Drop' of the Sprite / UI Element.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"OnDrag");

        // Drag and Drop Math:
        //
        if (_canvas != null)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        else
        {
            _rectTransform.anchoredPosition += eventData.delta;
            
        }//End if (_canvas != null)
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"OnEndDrag");

        #region OnDrop Validation: Out of Bounds of (GUI) UI_Inventory
        
        // 2- Validations:   Drop (Item) Out of Bounds.
        //
        if (_rectTransformOfInventoryGUI != null)
        {
            
            // Check if the UI Element is out of bounds of the GUI:
            //
            if ( ! _rectTransform.Overlaps( _rectTransformOfInventoryGUI ) )
            {
                // It is OUT OF BOUNDS !
                // The ITEM (GUI GameObject)  is being Dropped: "Out Of Bounds", on a blank space on screen:
                // Reposition the GUI Element(ITEM):
                //
                _rectTransform.anchoredPosition = _last2DPositionOfUIElementBeforeDragNDrop;
                
                // Debug Only. Comment Latter:
                //
                Debug.LogWarning($"-->VALIDATING DRAG AND DROP (Drop (Item) Out of Bounds.\n* The ITEM GUI is out of bounds.\n\nThis GameObject is:= {this.name}", this);

            }//End if (! _rectTransform.RectOverlaps( _rectTransformOfInventoryGUI ) )
            else
            {
                // The "Drag and Drop" execution: is valid
                
                // Skip:   this use case is being handled in another script that is on a GameObject with GUI info from the UI_SLOT, GUI for the SLOT:  ItemDropOnSlot.cs
                
            }//End else of if ( ! _rectTransform.Overlaps( _rectTransformOfInventoryGUI ) )

        }//End if (_rectTransform == null)

        #endregion OnDrop Validation: Out of Bounds of (GUI) UI_Inventory
        
        // After Validation:  Re-Enable Raycasts:
        //
        // Make it Semi-Transparent:
        //
        _canvasGroupOnThisGameObject.alpha = 1.0f;

        // UN-Block Interaction with ALL UI Elements on this GUI CANVAS  (Items children of this Canvas Group)
        //
        _canvasGroupOfParentGameObject.blocksRaycasts = true;
        
    }//End OnEndDrag()

    
    /// <summary>
    /// Whenever you (Right) Click. 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown");
        
        // 1- Save the initial Position of the UI Element:
        //
        _last2DPositionOfUIElementBeforeDragNDrop = _rectTransform.anchoredPosition;
    }
    

    #endregion Interfaces Methods



    #endregion My Custom Methods

}
