using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Set References")]
    [SerializeField] InputReader inputReader = null;

    [Header("UI Canvases")]
    [SerializeField] Canvas characterCanvas = null;
    [SerializeField] Canvas inventoryCanvas = null;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        inputReader.ToggleCharacterTabEvent += ToggleCharacterTab;
        inputReader.ToggleInventoryTabEvent += ToggleInventoryTab;
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleCharacterTab()
    {
        if(inventoryCanvas.enabled == true)
        {
            inventoryCanvas.enabled = false;
        }

        if(characterCanvas.enabled == false)
        {
            characterCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            characterCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }

        ExperienceManager.Instance.UpdateExperienceUI();
    }

    public void ToggleInventoryTab()
    {
        if(characterCanvas.enabled == true)
        {
            characterCanvas.enabled = false;
        }

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

    private void OnDisable()
    {
        inputReader.ToggleCharacterTabEvent -= ToggleCharacterTab;
        inputReader.ToggleInventoryTabEvent -= ToggleInventoryTab;
    }

}