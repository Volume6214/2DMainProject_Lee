using System.Collections.Generic;
using UnityEngine;

// ValueType1 : 아이템이 가진 벨류로 분류
public enum EItemInfoCategoryLineX
{
    None = 0,
    All,
    DamageCategory,
    ShieldCategory,
    HealCategory,
    RecoveryCategory,
    DebuffPoisonCategory,
    DebuffBurnCategory,
}

// ItemType : 아이템의 타입으로 분류
public enum EItemInfoCategoryLineY
{
    None = 0,
    All,
    WeaponCategory,
    CommonCategory,
    ArmorCategory,
    AccessoryCategory,
    ConsumableCategory,
    FoodCategory,
    ArtifactCategory,
    ToolCategory,
}

public class Std_ItemInfoUI : DaniTechUIBase
{
    [SerializeField] private GameObject _prefabSlot;
    [SerializeField] private Transform _slotRoot;

    private EItemInfoCategoryLineX _currentLineX = EItemInfoCategoryLineX.All;
    private EItemInfoCategoryLineY _currentLineY = EItemInfoCategoryLineY.All;

    private Dictionary<string, Std_ItemInfoSlotUI> _slotList = new Dictionary<string, Std_ItemInfoSlotUI>();

    public void OnClick_LineX(int index)
    {
        _currentLineX = (EItemInfoCategoryLineX)index;
        RefreshItemList();
    }

    public void OnClick_LineY(int index)
    {
        _currentLineY = (EItemInfoCategoryLineY)index;
        RefreshItemList();
    }

    private void RefreshItemList()
    {
        DestroyAndClearSlotList();

        // 매니저에서 전체 아이템 리스트를 가져옴
        var allItems = DaniTechGameDataManager.Instance.GetAllStdItemData(); 

        foreach (var data in allItems)
        {
            // 필터링 로직: All이거나 타입이 일치하면 true
            bool isMatchX = (_currentLineX == EItemInfoCategoryLineX.All) || (data.ValueType1 == _currentLineX.ToString());
            bool isMatchY = (_currentLineY == EItemInfoCategoryLineY.All) || (data.ItemType == _currentLineY.ToString());

            if (isMatchX && isMatchY)
            {
                CreateSlot(data);
            }
        }
    }

    private void CreateSlot(StdItemData data)
    {
        var gObj = Instantiate(_prefabSlot, _slotRoot);
        var slotComponent = gObj.GetComponent<Std_ItemInfoSlotUI>();

        if (slotComponent != null)
        {
            slotComponent.InitSlot(data, OnClickChildSlotSelected);

            _slotList.Add(data.Id, slotComponent);
        }
    }

    private void DestroyAndClearSlotList()
    {
        foreach (var slot in _slotList.Values)
        {
            if (slot != null) DestroyImmediate(slot.gameObject);
        }
        _slotList.Clear();
    }

    private void OnClickChildSlotSelected(string slotDataId)
    {
        // 1. 선택된 아이템의 상세 정보 표시 로직
        var data = DaniTechGameDataManager.Instance.GetStdItemData(slotDataId);
        if (data == null) return;
        Debug.Log($"선택한 아이템: {data.Name}");

        // 2. 선택된 슬롯 UI 효과 처리
        foreach (var slot in _slotList.Values)
        {
            slot.SetSelectedUI(slot.GetSlotDataId() == slotDataId);
        }
    }
}