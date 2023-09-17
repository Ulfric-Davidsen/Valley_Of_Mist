using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Assignment")]
    public InventoryObject inventory;
    public InventoryObject equipment;

    [Header("Attributes Array")]
    public Attribute[] attributes;

    private void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }

        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            inventory.Save();
            equipment.Save();
            Debug.Log("Saving");
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
            equipment.Load();
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
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed items: ", string.Join(", ", _slot.AllowedSlots)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }

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
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed items: ", string.Join(", ", _slot.AllowedSlots)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}