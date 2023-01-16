using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public LayerMask interactionLayer;
    public float interactionRadius = 1f;

    public bool isInteracting { get; private set; }

    private void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRadius, interactionLayer);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null) StartInteraction(interactable);

            }
        }
    }

    private void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        isInteracting = true;
    }

    //unused
    private void EndInteraction()
    {
        isInteracting = false;
    }
}
