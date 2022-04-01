using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDropHandler : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        var otherItemTranform = eventData.pointerDrag.transform;
        otherItemTranform.SetParent(transform);
        otherItemTranform.localPosition = Vector3.zero;
    }
}
