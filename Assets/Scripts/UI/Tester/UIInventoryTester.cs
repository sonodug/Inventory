using System.Collections.Generic;
using UnityEngine;

public class UIInventoryTester : MonoBehaviour
{
    private InventoryItemInfo _appleInfo;
    private InventoryItemInfo _pepperInfo;
    private UIInventorySlot[] _uiSlots;

    public InventoryWithSlots Inventory { get; }

    public UIInventoryTester(InventoryItemInfo appleInfo, InventoryItemInfo pepperInfo, UIInventorySlot[] uiSlots)
    {
        _appleInfo = appleInfo;
        _pepperInfo = pepperInfo;
        _uiSlots = uiSlots;

        Inventory = new InventoryWithSlots(18);
        Inventory.StateChanged += OnInventoryStateChanged;
    }

    public void FillSlots()
    {
        var allSlots = Inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 5;
        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomApplesItemIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomPeppersItemIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }

        SetupInventoryUI(Inventory);
    }

    private IInventorySlot AddRandomApplesItemIntoRandomSlot(List<IInventorySlot> slots)
    {
        int randomSlotIndex = Random.Range(0, slots.Count);
        var randomSlot = slots[randomSlotIndex];
        var randomCount = Random.Range(1, 4);
        var apple = new Apple(_appleInfo);
        apple.State.Amount = randomCount;
        Inventory.TryToAddToSlot(this, randomSlot, apple);
        return randomSlot;
    }

    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;

        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private IInventorySlot AddRandomPeppersItemIntoRandomSlot(List<IInventorySlot> slots)
    {
        int randomSlotIndex = Random.Range(0, slots.Count);
        var randomSlot = slots[randomSlotIndex];
        var randomCount = Random.Range(1, 4);
        var pepper = new Pepper(_pepperInfo);
        pepper.State.Amount = randomCount;
        Inventory.TryToAddToSlot(this, randomSlot, pepper);
        return randomSlot;
    }


    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}