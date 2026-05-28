using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Std_ItemInfoSlotUI : MonoBehaviour
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_MainName;
    [SerializeField] private GameObject GObj_Selected;
    [SerializeField] private DaniTechUIButton Button_SlotClick;

    public string DataId { get; private set; }
    public string ItemType { get; private set; } // Y라인
    public string ValueType1 { get; private set; } // X라인
    public int Size { get; private set; } // 사이즈(슬롯차지)

    private event Action<string> _onClickSlot; 
    private string _slotDataId;


    public string GetSlotDataId()
    {
        return _slotDataId;
    }

    private void OnEnable()
    {
        Button_SlotClick.BindOnClickButtonEvent(OnClick_ItemSlot);
    }

    private void OnClick_ItemSlot()
    {
        _onClickSlot?.Invoke(_slotDataId);
    }

    private void OnDisable()
    {
        _onClickSlot = null;
    }

    public void InitSlot(StdItemData data, Action<string> onClickCallback)
    {
        // 데이터를 슬롯에 보관
        DataId = data.Id;
        ItemType = data.ItemType;
        ValueType1 = data.ValueType1;
        Size = data.Size;
        _slotDataId = data.Id;

        // 콜백 등록
        _onClickSlot = onClickCallback;

        // UI 갱신
        SetSlotUI(data.Name, data.IconPath);

        // 나중에 배치 로직을 바꿀 때를 대비해 Size를 여기서 적용
        // SetSlotSize(Size); 
    }
    private void SetSlotUI(string dataName, string iconPath)
    {
        Text_MainName.text = dataName;
        if (!string.IsNullOrEmpty(iconPath))
        {
            DaniTechGameUtil.LoadAndSetSpriteImage(Image_MainIcon, iconPath).Forget();
        }
    }


    public void SetSelectedUI(bool isSelect)
    {
        if (GObj_Selected != null) GObj_Selected.SetActive(isSelect);
    }
}