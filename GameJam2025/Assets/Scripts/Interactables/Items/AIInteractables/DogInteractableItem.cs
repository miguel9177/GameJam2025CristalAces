using System.Collections;
using UnityEngine;

public class DogInteractableItem : InteractableItemBase
{
    [SerializeField] InventoryItemData itemNecessaryToMoveOutDog;
    [SerializeField] AI_PlayerManager dogNpcManager;
    [SerializeField] Animation animationComponent;

    protected override void _Start()
    {
        base._Start();
        dogNpcManager.BlockOrUnlockMovement(true);
        animationComponent.Play("Sit");
    }

    public override bool CanInteract()
    {
        return base.CanInteract();
    }

    protected override void Interact()
    {
        if(!GameplayManager.Instance.Player.InventoryManager.HasItem(itemNecessaryToMoveOutDog))
        {
            Debug.Log("YOU DONNOT HAVE THE NECESSARY ITEMS");
            return;
        }

        GameplayManager.Instance.Player.InventoryManager.ConsumeItem(itemNecessaryToMoveOutDog);
        dogNpcManager.BlockOrUnlockMovement(false);
        animationComponent.Play("Walk");
        StartCoroutine(IE_WaitUntillNoMoreMovement());
    }

    private void FinishedDogPuzzle()
    {
        animationComponent.Play("Sit");
    }

    private IEnumerator IE_WaitUntillNoMoreMovement()
    {
        yield return new WaitForSeconds(0.5f);
        while(dogNpcManager.Movement.HasTarget)
        {
            yield return new WaitUntil(() => dogNpcManager.Movement.HasTarget == false);
            yield return null;
        }

        FinishedDogPuzzle();
    }
}
