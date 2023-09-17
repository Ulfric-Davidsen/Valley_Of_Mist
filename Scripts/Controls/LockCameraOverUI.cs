using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class LockCameraOverUI : MonoBehaviour
{
    [SerializeField]
    private CinemachineInputProvider inputProvider;

    private void Start()
    {
        inputProvider.enabled = true;
    }

    private void Update()
    {
        if(IsMouseOverUI())
        {
            inputProvider.enabled = false;
        }
        else
        {
            inputProvider.enabled = true;
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
