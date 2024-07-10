using UnityEngine;

public abstract class Validater : MonoBehaviour
{
    [Tooltip("The order in which this validater will be checked. Lower numbers are checked first.")]
    public int checkOrder = 0;
    public abstract bool IsValid();
}