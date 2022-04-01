using System;
using UnityEngine;

public interface IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type { get; }

    IInventoryItem Clone();
}
