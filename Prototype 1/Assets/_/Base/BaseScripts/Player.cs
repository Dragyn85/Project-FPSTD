/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

// using System;
using UnityEngine;
// using V_AnimationSystem;
// using CodeMonkey.Utils;

/*
 * Player movement with Arrow keys
 * Attack with Space
 * */
public class Player : MonoBehaviour
{
    
    public static Player Instance { get; private set; }

    private const float SPEED = 50f;
    
    [SerializeField] private MaterialTintColor materialTintColor;
    [SerializeField] private UI_Inventory uiInventory;

    private Player_Base playerBase;
    private State state;
    private Inventory inventory;

    private enum State
    {
        Normal,
    }

    private void Awake()
    {
        Instance = this;
        playerBase = gameObject.GetComponent<Player_Base>();
        SetStateNormal();

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.GetItemType())
        {
            
            case Item.ItemType.HealthPotion:
                FlashGreen();
                
                // ACA_QUEDE
                
                // This creates problems. It is easier to just use the: inventory.RemoveItem(item);  (which triggers from Database an EVent that  the "Code Behind") the GUI picks-up and refreshes the GUI accordingly.
                // Not do: ...Item myItem = inventory.CreateNewItem(Item.ItemType.HealthPotion, 1);
                //
                // Previous implementation: inventory.RemoveItem( item /*myItem*/ );
                //
                inventory.RemoveItem( item.GetItemType() /*myItem*/ );

                    // Before it was:  inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            
            case Item.ItemType.ManaPotion:
                FlashBlue();
                
                // This creates problems. It is easier to just use the: inventory.RemoveItem(item);  (which triggers from Database an EVent that  the "Code Behind") the GUI picks-up and refreshes the GUI accordingly.
                // Not do: ...Item myItem2 = inventory.CreateNewItem(Item.ItemType.ManaPotion, 1);
                //
                // Previous implementation: inventory.RemoveItem( item /*myItem2*/ );
                //
                inventory.RemoveItem( item.GetItemType() /*myItem2*/ );
                
                    // Before itt was:  inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                break;
        }
    }
    
    private void SetStateNormal()
    {
        state = State.Normal;
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        bool isIdle = moveX == 0 && moveY == 0;
        
        if (isIdle)
        {
            playerBase.PlayIdleAnim();
            
        }
        else
        {
            playerBase.PlayMoveAnim(moveDir);
            transform.position += moveDir * (SPEED * Time.deltaTime);
        }
    }

    private void DamageFlash()
    {
        materialTintColor.SetTintColor(new Color(1, 0, 0, 1f));
    }

    public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance)
    {
        transform.position += knockbackDir * knockbackDistance;
        DamageFlash();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void FlashGreen()
    {
        materialTintColor.SetTintColor(new Color(0, 1, 0, 1));
    }

    public void FlashRed()
    {
        materialTintColor.SetTintColor(new Color(1, 0, 0, 1));
    }

    public void FlashBlue()
    {
        materialTintColor.SetTintColor(new Color(0, 0, 1, 1));
    }
        
}
