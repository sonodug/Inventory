using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _appleInfo;
    [SerializeField] private InventoryItemInfo _pepperInfo;

    private UIInventoryTester tester;

    public InventoryWithSlots Inventory => tester.Inventory;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        tester = new UIInventoryTester(_appleInfo, _pepperInfo, uiSlots);
        tester.FillSlots();
    }
}
