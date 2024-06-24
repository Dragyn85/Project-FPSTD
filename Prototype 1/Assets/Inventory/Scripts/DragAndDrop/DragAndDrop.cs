/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
{

    #region Attributes

    //  CONSTANTS
    private float _CANVAS_GROUP_ALPHA_SEMITRANSPARENT = 0.5f;

    // Variables
    
    [Tooltip("GUI Canvas for extracting information: the UI Canvas Scale.")]
    [SerializeField]
    private Canvas _canvas;    
    
    [Tooltip("Rect Transform of the UI Visual Element we want to move around the screen (by Drag And Dropping it...).")]
    [SerializeField]
    private RectTransform _rectTransform;
   
    [Tooltip("GUI Canvas Group: for blocking rayscast (i.e.: Mouse Pointer interaction) to this Item momentarily.")]
    [SerializeField]
    private CanvasGroup _canvasGroup;


    #endregion Attributes


    #region Unity Methods

    /// <summary>
    /// Awake is called before the Start calls round
    /// </summary>
    private void Awake()
    {
        // Get the UI Element to Drag and Drop.
        //
        _rectTransform = GetComponent<RectTransform>();

        if (_rectTransform == null)
        {
        
            Debug.LogError($"There's no RectTransform in this GameObject... for DRAG AND DROP.\n\nThis GameObject is:= {this.name}", this);
        }
        
        // Get the Canvas Group Component or Add one.
        //
        _canvasGroup = GetComponent<CanvasGroup>();
        //
        // Add one CanvasGroup ?
        //
        if (_canvasGroup == null)
        {
        
            Debug.LogWarning($"There's no CanvasGroup in this GameObject... for DRAG AND DROP.\n\nThis GameObject is:= {this.name}... Adding one", this);
            
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

    }//End Awake


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>



    /// <summary>
    /// Update is called once per frame
    /// </summary>


    #endregion Unity Methods
    

    #region My Custom Methods

    #region Interfaces Methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag");

        // Make it Semi-Transparent:
        //
        _canvasGroup.alpha = _CANVAS_GROUP_ALPHA_SEMITRANSPARENT;
        
        // Block Interaction with this UI Element:
        //
        _canvasGroup.blocksRaycasts = false;
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
        
        // Make it 100% Opaque again:
        //
        _canvasGroup.alpha = 1.0f;
        
        // UN-Block Interaction with this UI Element:
        //
        _canvasGroup.blocksRaycasts = true;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointerDown");

    }
    

    #endregion Interfaces Methods



    #endregion My Custom Methods

}
