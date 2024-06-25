/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler /*, IPointerEnterHandler, IPointerExitHandler */ 
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

            // 1- Place the "UI Element" correctly inside the "UI Slot-Element" 
            //
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // 2- Get the Reference to the "UI Slot-Element" (is this class-object) and trigger an Update / on the Inventory Database in Memory.           
            //
        }
        else
        {
            Debug.LogWarning($"Error on --->OnDrop");
        }
        
    }

    
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
