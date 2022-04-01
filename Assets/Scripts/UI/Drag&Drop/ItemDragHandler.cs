using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTranform;

    private void Start()
    {
        _rectTranform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTranform = _rectTranform.parent;
        slotTranform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTranform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}
