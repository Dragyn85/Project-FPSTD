using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class PlacablePlacer : MonoBehaviour
{
    private Placeable placeable;
    private bool isPlacing;
    private PlayerInputs playerInputs;

    [SerializeField] private Placeable placeableInstance;

    public void SetPlayerInputs(PlayerInputs playerInputs)
    {
        this.playerInputs = playerInputs;
    }

    public void StartPlacement(Placeable placeable)
    {
        this.placeable = placeable;
        isPlacing = true;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartPlacement(placeableInstance);
        }

        if (!isPlacing)
        {
            return;
        }

        //make a ray from the center of the screen in the forward direction
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        
        if (Physics.Raycast(ray, out var hit))
        {
            placeable.SetPlaceHolderPosition(hit.point);
            bool isValid = placeable.IsValidPlacement(hit.point);

            if (isValid)
            {
                placeable.SetPlaceHolderColor(Color.green);
            }
            else
            {
                placeable.SetPlaceHolderColor(Color.red);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (placeable.TryPlace())
                {
                    isPlacing = false;
                    Destroy(placeableInstance.gameObject);
                }
            }
        }
    }
}