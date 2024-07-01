/* NOTE: Modified Unity C# Script Template by Alec AlMartson...
...on Path:   /PathToUnityHub/Unity/Hub/Editor/UNITY_VERSION_FOR_EXAMPLE__6000.0.3f1/Editor/Data/Resources/ScriptTemplates/1-Scripting__MonoBehaviour Script-NewMonoBehaviourScript.cs.txt
*/
using UnityEngine;

/// <summary>
/// This Class handles extension Methods for "RectTransforms" (legacy GUI in Unity3D).
/// </summary>
public static class ExtensionMethod
{

    #region Attributes
    #endregion Attributes

    #region Unity Methods
    #endregion Unity Methods
    

    #region My Custom Methods

    /// <summary>
    /// Checks whether two UI Panels on my Unity Canvas are overlapping each other.
    /// </summary>
    /// <param name="rectTrans1"></param>
    /// <param name="rectTrans2"></param>
    /// <returns></returns>
    public static bool RectOverlaps(this RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);

    }// End RectOverlaps()



    #endregion My Custom Methods

}
