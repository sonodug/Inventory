using System;
using UnityEngine;

public class Apple : IInventoryItem
{
    public IInventoryItemInfo Info { get; }

    public IInventoryItemState State { get; }

    public Type Type => GetType();

    public Apple(IInventoryItemInfo info)
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
