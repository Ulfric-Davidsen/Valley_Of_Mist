using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableInventory : MonoBehaviour
{
    [Header("Inventory Assignment")]
    public InventoryObject inventory;

    [Header("Available Loot Array")]
    public ItemObject[] items;

    private void Start()
    {
        foreach (ItemObject item in items)
        {
            Item _item = new Item(item);
            inventory.AddItem(_item, 1);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            inventory.Save();
            Debug.Log("Saving");
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
            Debug.Log("Loading");
        }
    }

    // public void OnTriggerEnter(Collider other)
    // {
    //     var interactableItem = other.GetComponent<InteractableItem>();
    //     if(interactableItem)
    //     {
    //         Item _item = new Item(interactableItem.item);
    //         if(inventory.AddItem(_item, 1))
    //         {
    //             Destroy(other.gameObject);
    //         }
    //     }
    // }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
        {
            return;
        }

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
        {
            return;
        }
        
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}