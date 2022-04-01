using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : SlotDropHandler
{
    [SerializeField] private UIInventoryItem _uiinventoryItem;

    private UIInventory _uIInventory;
    public IInventorySlot Slot { get; private set; }

    public void SetSlot(IInventorySlot newSlot)
    {
        Slot = newSlot;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.Slot;
        var inventory = _uIInventory.Inventory;

        inventory.TransitFromSlotToSlot(this, otherSlot, Slot);

        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (Slot != null)
            _uiinventoryItem.Refresh(Slot);
    }

    private void Awake()
    {
        _uIInventory = GetComponentInParent<UIInventory>();
    }
}
