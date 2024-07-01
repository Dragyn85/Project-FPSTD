/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;

/// <summary>
/// This Class handles Extension Methods for "RectTransforms" (legacy GUI in Unity3D).
/// </summary>
public static class ExtensionMethod
{

    #region Attributes
    #endregion Attributes

    #region Unity Methods
    #endregion Unity Methods
    

    #region My Custom Methods
    
    #region GUI RectTrannsform Overlapping
    
    /// <summary>
    /// Checks whether two UI Panels on my Unity Canvas are overlapping each other. <br/> <br/>
    /// 
    /// Idea taken from Stackoverflow:  https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
    /// </summary>
    /// <param name="firstRectTransform"></param>
    /// <param name="secondRectTransform"></param>
    /// <returns></returns>
    public static bool Overlaps(this RectTransform firstRectTransform, RectTransform secondRectTransform)
    {
        return firstRectTransform.WorldRect().Overlaps(secondRectTransform.WorldRect());

    }// End Overlaps()
    
    
    /// <summary>
    /// Checks whether two UI Panels on my Unity Canvas are overlapping each other; but this one also takes into consideration the possibility of having negative numbers in the: widths and heights of the Rects. <br/> <br/>
    /// 
    /// Idea taken from Stackoverflow:  https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
    /// </summary>
    /// <param name="firstRectTransform"></param>
    /// <param name="secondRectTransform"></param>
    /// <param name="allowInverse"></param>
    /// <returns></returns>
    public static bool Overlaps(this RectTransform firstRectTransform, RectTransform secondRectTransform, bool allowInverse)
    {

        return firstRectTransform.WorldRect().Overlaps(secondRectTransform.WorldRect(), allowInverse);
        
    }// End Overlaps()


    /// <summary>
    /// Utility Function for: Converts the RectTransform to World Coordinates, so we can check if it Overlaps over another RectTransform. <br/> <br/>
    /// 
    /// Idea taken from Stackoverflow:  https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns></returns>
    public static Rect WorldRect(this RectTransform rectTransform)
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        Vector2 pivot = rectTransform.pivot;

        Vector3 lossyScale = rectTransform.lossyScale;
        float rectTransformWidth = sizeDelta.x * lossyScale.x;
        float rectTransformHeight = sizeDelta.y * lossyScale.y;

        // With this it works even if the pivot is not at the center
        //
        Vector3 position = rectTransform.TransformPoint(rectTransform.rect.center);
        float x = position.x - rectTransformWidth * 0.5f;
        float y = position.y - rectTransformHeight * 0.5f;
            
        return new Rect(x,y, rectTransformWidth, rectTransformHeight);

    }// End WorldRect()
    
    #endregion GUI RectTrannsform Overlapping

    
    #endregion My Custom Methods

}
