using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Std_ItemSlotUI : DaniTechUIBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("슬롯 UI 요소")]
    [SerializeField] private Image Image_Icon;
    [SerializeField] private Text Text_Value1;

    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    private StdItemData _currentItemData;

    public int ItemSize = 1;
    public int CurrentSlotIndex = -1;

    public string ItemDataId;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetItemData(StdItemData data)
    {
        _currentItemData = data;

        if (_currentItemData != null)
        {
            Text_Value1.text = _currentItemData.Value1.ToString();
            Debug.Log($"전투 슬롯에 아이템 장착: {_currentItemData.Value1}");
        }
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

    public void Setup(string dataId, int size)
    {
        ItemDataId = dataId;
        ItemSize = size;
    }

}