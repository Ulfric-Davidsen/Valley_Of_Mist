using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("Assign Item")]
    public ItemObject item;

    [Header("UI Display")]

    [SerializeField]
    private GameObject iconCanvas;
    // [SerializeField]
    // private Image fillImage;
    // [SerializeField]
    // private float fillTime = 1f;

    [Header("Interaction Variables")]

    [SerializeField]
    private bool hasDuration = true;
    [SerializeField]
    private float interactionDuration;

    private PlayerInventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerInventory>(out PlayerInventory _playerInventory))
        {
            playerInventory = _playerInventory;
            ShowCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<PlayerInventory>(out PlayerInventory _playerInventory))
        {
            HideCanvas();
        }
    }

    private void ShowCanvas()
    {
        iconCanvas.SetActive(true);
    }

    private void HideCanvas()
    {
        iconCanvas.SetActive(false);
    }

    private void AddItemToInventory()
    {
        if(playerInventory == null)
        {
            return;
        }
        else
        {
            Item _item = new Item(item);
            if(playerInventory.inventory.AddItem(_item, 1))
            {
                Destroy(gameObject);
            }
        }
    }

    public void Interact(Transform interactorTransform)
    {
        AddItemToInventory();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool HasDuration()
    {
        return hasDuration;
    }

    public float GetDuration()
    {
        return interactionDuration;
    }
}