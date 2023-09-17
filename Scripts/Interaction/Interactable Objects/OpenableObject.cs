using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenableObject : MonoBehaviour, IInteractable
{
    [Header("UI Display")]

    [SerializeField]
    private GameObject iconCanvas;
    [SerializeField]
    Canvas inventoryCanvas = null;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private float fillTime = 1f;

    [Header("Interaction Variables")]

    [SerializeField]
    private bool hasDuration = true;
    [SerializeField]
    private float interactionDuration;

    private bool isInteracting = false;
    private bool isOpen = false;

    private void Update()
    {
        if(isInteracting)
        {
            if(!isOpen)
            {
                IncreaseIconFillAmount();
            }
            else if(isOpen)
            {
                DecreaseIconFillAmount();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShowIconCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HideIconCanvas();
        }
    }

    private void ShowIconCanvas()
    {
        iconCanvas.SetActive(true);
    }

    private void HideIconCanvas()
    {
        iconCanvas.SetActive(false);
    }

    public void ToggleInventoryTab()
    {
        if(inventoryCanvas.enabled == false)
        {
            inventoryCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            inventoryCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void IncreaseIconFillAmount()
    {
        fillImage.fillAmount += 1.0f / fillTime * Time.deltaTime;

        if(fillImage.fillAmount == 1f)
        {
            isOpen = true;
            Debug.Log("OPENED");
            //Put open animation call here
            //Trigger action here
            ToggleInventoryTab();
            isInteracting = false;
            Debug.Log("INTERACTION ENDED");
        }
    }

    private void DecreaseIconFillAmount()
    {
        fillImage.fillAmount -= 1.0f / fillTime * Time.deltaTime;

        if(fillImage.fillAmount == 0)
        {
            isOpen = false;
            Debug.Log("CLOSED");
            //Put close animation call here
            //Trigger action here
            ToggleInventoryTab();
            isInteracting = false;
            Debug.Log("INTERACTION ENDED");
        }
    }

    private void ResetIconFillAmount()
    {
        if(!isOpen && fillImage.fillAmount > 0)
        {
            fillImage.fillAmount = 0;
        }

        if(isOpen && fillImage.fillAmount < 1)
        {
            fillImage.fillAmount = 1;
        }
    }

    public void Interact(Transform interactorTransform)
    {
        isInteracting = true;
        ResetIconFillAmount();
        Debug.Log("INTERACTION STARTED");
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
