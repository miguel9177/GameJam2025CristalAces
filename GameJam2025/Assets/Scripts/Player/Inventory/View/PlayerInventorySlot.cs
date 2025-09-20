using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    private bool filled = false;
    private InventoryItemData itemData;

    public bool Filled => filled;

    private void Start()
    {
        icon.gameObject.SetActive(false);
    }

    public void Initialize(InventoryItemData itemHere)
    {
        if (itemHere == null)
            return;

        itemData = itemHere;
        filled = true;

        icon.gameObject.SetActive(true); 
        icon.sprite = itemHere.Icon;
    }

    public bool HasItem(InventoryItemData item)
    {
        return itemData == item;
    }

    public void ConsumeItem()
    {
        filled = false;
        icon.gameObject.SetActive(false);
        itemData = null;
        icon.sprite = null;
    }
}
