using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItemData", order = 1)]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private InteractableItemBase prefab;
    [SerializeField] private Sprite icon;

    public Sprite Icon => icon;
}
