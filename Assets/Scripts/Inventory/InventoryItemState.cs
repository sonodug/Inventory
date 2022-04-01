using System;
using UnityEngine;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    public int ItemAmount;
    public bool IsItemEquipped;

    public int Amount { get => ItemAmount; set => ItemAmount = value; }
    public bool IsEquipped { get => IsItemEquipped; set => IsItemEquipped = value; }
}
