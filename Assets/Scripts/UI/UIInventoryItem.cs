using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryItem : ItemDragHandler
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;

    public IInventoryItem Item { get; private set; }

    public void Refresh(IInventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            CleanUp();
            return;
        }

        Item = slot.Item;
        _imageIcon.sprite = Item.Info.Icon;
        _imageIcon.gameObject.SetActive(true);

        var textAmountEnabled = slot.Amount > 1;
        _textAmount.gameObject.SetActive(textAmountEnabled);

        if (textAmountEnabled)
            _textAmount.text = $"x{slot.Amount.ToString()}";
    }

    private void CleanUp()
    {
        _textAmount.gameObject.SetActive(false);
        _imageIcon.gameObject.SetActive(false);
    }
}