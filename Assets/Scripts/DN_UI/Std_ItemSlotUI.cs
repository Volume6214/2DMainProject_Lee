using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Std_ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    public int ItemSize = 1;
    public int CurrentSlotIndex = -1;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentSlotIndex != -1)
        {
            StdInventoryManager.Instance.RemoveItem(CurrentSlotIndex, ItemSize);
            CurrentSlotIndex = -1;
        }

        _originalParent = transform.parent;
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == transform.root)
        {
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;

            var dropSlot = _originalParent.GetComponent<Std_ItemDropHomeUI>();
            if (dropSlot != null)
            {
                CurrentSlotIndex = dropSlot.SlotIndex;
                StdInventoryManager.Instance.PlaceItem(CurrentSlotIndex, ItemSize);
            }
        }
    }
}