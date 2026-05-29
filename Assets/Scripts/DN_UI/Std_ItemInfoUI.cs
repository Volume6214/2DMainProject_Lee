using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Std_ItemInfoUI : DaniTechUIBase
{
    [Header("동적 생성할 프리팹")]
    [SerializeField] private GameObject _prefabSlot;

    [Header("디테일 정보 영역")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_MainName;
    [SerializeField] private Text Text_Description;

    [Header("기타 정보")]
    [SerializeField] private Transform _slotRoot;
    [SerializeField] private DaniTechUIButton Button_CloseStdItemInfoUI;

    [Header("상단 카테고리 버튼들")]
    [SerializeField] private DaniTechUIButton Button_All;
    [SerializeField] private DaniTechUIButton Button_Weapon;
    [SerializeField] private DaniTechUIButton Button_Companion;
    [SerializeField] private DaniTechUIButton Button_Armor;
    [SerializeField] private DaniTechUIButton Button_Accessory;
    [SerializeField] private DaniTechUIButton Button_Tool;
    [SerializeField] private DaniTechUIButton Button_Consumable;
    [SerializeField] private DaniTechUIButton Button_Food;
    [SerializeField] private DaniTechUIButton Button_Artifact;
    [SerializeField] private DaniTechUIButton Button_Toy;

    private Dictionary<string, Std_ItemInfoSlotUI> _slotList = new Dictionary<string, Std_ItemInfoSlotUI>();

    private void OnEnable()
    {
        Button_All.BindOnClickButtonEvent(OnClick_All);
        Button_Weapon.BindOnClickButtonEvent(OnClick_Weapon);
        Button_Companion.BindOnClickButtonEvent(OnClick_Companion);
        Button_Armor.BindOnClickButtonEvent(OnClick_Armor);
        Button_Accessory.BindOnClickButtonEvent(OnClick_Accessory);
        Button_Tool.BindOnClickButtonEvent(OnClick_Tool);
        Button_Consumable.BindOnClickButtonEvent(OnClick_Consumable);
        Button_Food.BindOnClickButtonEvent(OnClick_Food);
        Button_Artifact.BindOnClickButtonEvent(OnClick_Artifact);
        Button_Toy.BindOnClickButtonEvent(OnClick_Toy);

        Button_CloseStdItemInfoUI.BindOnClickButtonEvent(OnClick_CloseStdItemInfoUI);

        StartCoroutine(WaitForDataAndInitialize());

    }

    private void OnDisable()
    {
        ClearAllSlots();
    }

    private void OnClick_All()
    {
        FilterAndCreateSlots("All");
    }
    private void OnClick_Weapon()
    {
        FilterAndCreateSlots("Weapon");
    }
    private void OnClick_Companion()
    {
        FilterAndCreateSlots("Companion");
    }
    private void OnClick_Armor()
    {
        FilterAndCreateSlots("Armor");
    }
    private void OnClick_Accessory()
    {
        FilterAndCreateSlots("Accessory");
    }
    private void OnClick_Tool()
    {
        FilterAndCreateSlots("Tool");
    }
    private void OnClick_Consumable()
    {
        FilterAndCreateSlots("Consumable");
    }
    private void OnClick_Food()
    {
        FilterAndCreateSlots("Food");
    }
    private void OnClick_Artifact()
    {
        FilterAndCreateSlots("Artifact");
    }
    private void OnClick_Toy()
    {
        FilterAndCreateSlots("Toy");
    }

    public void OnClick_CloseStdItemInfoUI()
    {
        Debug.Log("Close 버튼 클릭됨!");
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.Std_ItemInfoUI);
    }

    private System.Collections.IEnumerator WaitForDataAndInitialize()
    {
        while (DaniTechGameDataManager.Instance == null ||
               DaniTechGameDataManager.Instance.StdItemDataList == null)
        {
            yield return null; 
        }

        FilterAndCreateSlots("All");
    }

    private void FilterAndCreateSlots(string targetType)
    {
        ClearAllSlots();
        var allDataList = DaniTechGameDataManager.Instance.StdItemDataList;

        if (allDataList == null) return;

        foreach (var dataKv in allDataList)
        {
            var data = dataKv.Value;
            if (data == null) continue;
            if (targetType != "All" && data.ItemType != targetType) continue;

            CreateSlot(data);
        }
    }

    private void CreateSlot(StdItemData data)
    {
        if (_prefabSlot == null) return;

        var gObj = Instantiate(_prefabSlot, _slotRoot);
        var slot = gObj.GetComponent<Std_ItemInfoSlotUI>();

        if (slot != null)
        {
            slot.InitSlot(data, OnSlotSelected);
            _slotList.Add(data.Id, slot);

            Debug.Log($"슬롯 생성 완료: {data.Name}, Slot Component: {slot != null}");
        }
        else
        {
            Debug.LogError("생성된 슬롯에 Std_ItemInfoSlotUI 컴포넌트가 없습니다!");
        }
    }

    private void OnSlotSelected(StdItemData data)
    {
        Debug.Log($"[디테일 갱신] 아이템 이름: {data.Name}, 설명: {data.Description}");
        Text_MainName.text = data.Name;
        Text_Description.text = data.Description;

        if (!string.IsNullOrEmpty(data.IconPath))
        {
            DaniTechGameUtil.LoadAndSetSpriteImage(Image_MainIcon, data.IconPath).Forget();
        }
        Image_MainIcon.gameObject.SetActive(!string.IsNullOrEmpty(data.IconPath));

        foreach (var slot in _slotList.Values)
        {
            slot.SetSelectedUI(false);
        }
        if (_slotList.ContainsKey(data.Id))
            _slotList[data.Id].SetSelectedUI(true);
    }

    private void ClearAllSlots()
    {
        foreach (Transform child in _slotRoot)
        {
            Destroy(child.gameObject);
        }
        _slotList.Clear();
    }


}