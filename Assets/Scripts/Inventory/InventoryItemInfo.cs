using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Items/Create New ItemInfo", order = 51)]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _maxSlotItem;
    [SerializeField] private Sprite _icon;

    public string Id => _id;
    public string Title => _title;
    public string Description => _description;
    public int MaxSlotItems => _maxSlotItem;
    public Sprite Icon => _icon;
}
