using UnityEngine;

public class InteractablePickableItem : InteractableItemBase
{
    [SerializeField] InventoryItemData data;

    protected override void Interact()
    {
        base.Interact();

        bool added = GameplayManager.Instance.Player.InventoryManager.AddItemToSlots(data);
        if (added)
            AdddedItem();
    }

    protected virtual void AdddedItem()
    {
        Destroy(this.gameObject);
    }
}
