using UnityEngine;

public class DogInteractableItem : InteractableItemBase
{
    [SerializeField] InventoryItemData itemNecessaryToMoveOutDog;
    [SerializeField] AI_PlayerManager dogNpcManager;

    protected override void _Start()
    {
        base._Start();
        dogNpcManager.BlockOrUnlockMovement(true);
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
    }
}
