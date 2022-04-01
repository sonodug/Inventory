using System;
using UnityEngine;

public class Pepper : IInventoryItem
{
    public IInventoryItemInfo Info { get; }

    public IInventoryItemState State { get; }

    public Type Type => GetType();

    public Pepper(IInventoryItemInfo info)
    {
        this.Info = info;
        State = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clonedApple = new Apple(Info);
        clonedApple.State.Amount = State.Amount;
        return clonedApple;
    }
}
