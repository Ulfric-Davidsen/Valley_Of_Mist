using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable interactable;
    private float interactRange = 1f;
    private float currentInteractionDuration;
    private bool interactionHasDuration;

    public void Interact()
    {
        interactable = GetInteractableObject();
        if(interactable != null)
        {
            interactable.Interact(transform);
        }
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if(closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) <
                   Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                   {
                        closestInteractable = interactable;
                   }
            }
        }

        return closestInteractable;
    }

    public bool InteractionHasDuration()
    {
        interactionHasDuration = interactable.HasDuration();
        return interactionHasDuration;
    }

    public float GetCurrentInteractionDuration()
    {
        currentInteractionDuration = interactable.GetDuration();
        return currentInteractionDuration;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}