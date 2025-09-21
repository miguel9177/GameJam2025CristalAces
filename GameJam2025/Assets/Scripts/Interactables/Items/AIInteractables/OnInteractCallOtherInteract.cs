using UnityEngine;

public class OnInteractCallOtherInteract : InteractableItemBase
{
    [SerializeField] private InteractableItemBase interactableToCall;
    protected override void Interact()
    {
        base.Interact();
        interactableToCall.ForceCallInteract();
    }
}
