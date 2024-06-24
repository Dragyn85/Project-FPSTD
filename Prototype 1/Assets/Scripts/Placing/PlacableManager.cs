using UnityEngine;
using UnityEngine.Serialization;

public class PlacableManager : MonoBehaviour
{
    private Placeable placeable;
    private bool isPlacing;
    private PlayerInputs playerInputs;

    IPlace placeStrategy;

    [FormerlySerializedAs("debugPlacable")]
    [Header("Debugging")]
    [SerializeField] private Placeable debugPlacableTurret;
    [SerializeField] private Placeable debugPlacableWall;
    [SerializeField] private bool isDebugging;
    [SerializeField] private KeyCode placeTurret = KeyCode.Alpha3;
    [SerializeField] private KeyCode placeWall = KeyCode.Alpha4;
    

    
    private void Awake()
    {
        placeStrategy = new DesktopPlacementStartegy();
    }

    public void SetPlayerInputs(PlayerInputs playerInputs)
    {
        this.playerInputs = playerInputs;
    }

    void StartPlacement(Placeable placeable)
    {
        if (isPlacing)
        {
            return;
        }

        this.placeable = Instantiate(placeable);
        isPlacing = true;
    }


    private void LateUpdate()
    {
        HandleDebugging();

        if (!isPlacing)
        {
            return;
        }

        UpdatePlaceablePosition();
        HandlePlaceableRotation();
        HandleFinalPositioning();
        HandlePlacementCancelation();
    }

    private void HandleDebugging()
    {
        if (!isDebugging) return;
        
        if (Input.GetKeyDown(placeTurret))
        {
            StartPlacement(debugPlacableTurret);
        }

        if (Input.GetKeyDown(placeWall))
        {
            StartPlacement(debugPlacableWall);
        }
    }

    private void HandlePlaceableRotation()
    {
        if(placeStrategy.ShouldRotate())
        {
            placeable.Rotate();
        }
    }

    private void HandlePlacementCancelation()
    {
        if (placeStrategy.ShouldCancelPlacement())
        {
            Destroy(placeable.gameObject);
            isPlacing = false;
            placeable = null;
        }
    }

    private void HandleFinalPositioning()
    {
        if (placeStrategy.ShouldTryPlace())
        {
            if (placeable.TryPlace())
            {
                isPlacing = false;
                Destroy(placeable.gameObject);
            }
        }
    }

    private void UpdatePlaceablePosition()
    {
        var pos = placeStrategy.GetNewPlaceablePosition();
        placeable.SetPlaceHolderPosition(pos);
        bool isValid = placeable.IsValidPlacement(pos);

        if (isValid)
        {
            placeable.SetPlaceHolderColor(Color.green);
        }
        else
        {
            placeable.SetPlaceHolderColor(Color.red);
        }
    }
}