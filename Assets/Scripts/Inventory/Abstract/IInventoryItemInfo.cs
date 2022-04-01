using UnityEngine;

public interface IInventoryItemInfo
{
    public string Id { get; }
    public string Title { get; }
    public string Description { get; }
    public int MaxSlotItems { get; }
    public Sprite Icon { get; }
}
