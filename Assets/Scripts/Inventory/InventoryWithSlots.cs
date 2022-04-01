using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Refactoring: a lot of code duplication, the maximum number of items is the responsibility of the slot, clear var types.

public class InventoryWithSlots : IInventory
{
    private List<InventorySlot> _slots;
    
    public int Capacity { get; set; }
    public bool IsFull => _slots.All(slot => slot.IsFull);

    public event Action<object, IInventoryItem, int> ItemAdded;
    public event Action<object, Type, int> ItemRemoved;
    public event Action<object> StateChanged;

    public InventoryWithSlots(int capacity)
    {
        this.Capacity = capacity;

        _slots = new List<InventorySlot>(capacity);

        for (int i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot());
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.ItemType == itemType).Item;
    }

    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.IsEmpty)
                allItems.Add(slot.Item);
        }

        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);

        foreach (var slot in slotsOfType)
        {
            if (!slot.IsEmpty)
                allItemsOfType.Add(slot.Item);
        }

        return allItemsOfType.ToArray();

    }

    public IInventoryItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);
        var equippedItems = new List<IInventoryItem>();

        foreach(var slot in requiredSlots)
            equippedItems.Add(slot.Item);

        return equippedItems.ToArray();
    }

    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItemSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);

        foreach (var slot in allItemSlots)
            amount += slot.Amount;
        
        return amount;
    }
    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemNotEmpty = _slots.Find(slot => !slot.IsEmpty && slot.Item == item && !slot.IsFull);

        if (slotWithSameItemNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.IsEmpty);
        if (emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        return false;
    }

    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        if (slotsWithItem.Length == 0)
            return;

        int amountToRemove = amount;
        int count = slotsWithItem.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];

            if (slot.Amount >= amountToRemove)
            {
                slot.Item.State.Amount -= amountToRemove;

                if (slot.Amount == 0)
                    slot.Clear();

                ItemRemoved?.Invoke(sender, itemType, amountToRemove);
                StateChanged?.Invoke(sender);
                break;
            }

            var amountAfterRemove = slot.Amount;
            amountToRemove -= slot.Amount;
            slot.Clear();
            ItemRemoved?.Invoke(sender, itemType, amountAfterRemove);
            StateChanged?.Invoke(sender);
        }
    }


    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var IsFit = slot.Amount + item.State.Amount <= item.Info.MaxSlotItems;

        int amountToAdd = IsFit ? item.State.Amount : item.Info.MaxSlotItems - slot.Amount;
        int amountLeft = item.State.Amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.State.Amount = amountToAdd;

        if (slot.IsEmpty)
            slot.SetItem(clonedItem);
        else
            slot.Item.State.Amount += amountToAdd;

        ItemAdded?.Invoke(sender, item, amountToAdd);
        StateChanged?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.State.Amount = amountLeft;

        return TryToAdd(sender, item);
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.IsEmpty)
            return;
        if (toSlot.IsFull)
            return;
        if (!toSlot.IsEmpty && fromSlot.ItemType != toSlot.ItemType)
            return;

        int slotCapacity = fromSlot.Capacity;
        bool isFit = fromSlot.Amount + toSlot.Amount <= slotCapacity;
        int amountToAdd = isFit ? fromSlot.Amount : slotCapacity - toSlot.Amount;
        int amountLeft = fromSlot.Amount - amountToAdd;

        if (toSlot.IsEmpty)
        {
            toSlot.SetItem(fromSlot.Item);
            fromSlot.Clear();

            StateChanged?.Invoke(sender);
        }

        toSlot.Item.State.Amount += amountToAdd;
        if (isFit)
            fromSlot.Clear();
        else
            fromSlot.Item.State.Amount = amountLeft;
        
        StateChanged?.Invoke(sender);
    }

    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);

        return item != null;
    }

    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
    }

    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}
