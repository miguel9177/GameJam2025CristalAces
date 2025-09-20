using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] List<PlayerInventorySlot> allSlots = new List<PlayerInventorySlot>();

    public bool AddItemToSlots(InventoryItemData item)
    {
        for(int i = 0; i < allSlots.Count; i++)
        {
            if (allSlots[i].Filled == true)
                continue;

            allSlots[i].Initialize(item);
            return true;
        }

        return false;
    }

    public bool HasItem(InventoryItemData item)
    {
        for (int i = 0; i < allSlots.Count; i++)
        {
            if (allSlots[i].HasItem(item))
                return true;
        }

        return false;
    }

    public bool ConsumeItem(InventoryItemData itemNecessaryToMoveOutDog)
    {
        for(int i = 0; i < allSlots.Count; i++)
        {
            if (allSlots[i].HasItem(itemNecessaryToMoveOutDog))
            {
                allSlots[i].ConsumeItem();
                return true;
            }
        }

        return false;
    }
}
