using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{
    public bool IsFull => !IsEmpty && Amount == Capacity;

    public bool IsEmpty => Item == null;

    public IInventoryItem Item { get; private set; }

    public Type ItemType => Item.Type;

    public int Amount => IsEmpty ? 0 : Item.State.Amount;

    public int Capacity { get; private set; }

    public void SetItem(IInventoryItem item)
    {
        if (!IsEmpty)
            return;

        this.Item = item;
        this.Capacity = item.Info.MaxSlotItems;
    }
    public void Clear()
    {
        if (IsEmpty)
            return;

        Item.State.Amount = 0;
        Item = null;  
    }
}
