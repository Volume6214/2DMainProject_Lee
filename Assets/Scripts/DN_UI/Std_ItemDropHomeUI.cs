using UnityEngine;
using UnityEngine.EventSystems;

public class Std_ItemDropHomeUI : MonoBehaviour, IDropHandler
{
    public int SlotIndex;

    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<Std_ItemSlotUI>();
        if (item == null) return;

        if (StdInventoryManager.Instance.CanPlaceItem(SlotIndex, item.ItemSize))
        {
            StdInventoryManager.Instance.PlaceItem(SlotIndex, item.ItemSize);

            item.transform.SetParent(this.transform);
            item.transform.localPosition = Vector3.zero;

            item.CurrentSlotIndex = SlotIndex;
        }
    }
}