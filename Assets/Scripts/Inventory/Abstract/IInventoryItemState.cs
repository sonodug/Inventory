using UnityEngine;

public interface IInventoryItemState
{
    public int Amount { get; set; }
    public bool IsEquipped { get; set; }
}
