using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;


public class Std_ItemInfoSlotUI : MonoBehaviour
{
    [Header("슬롯 기본 정보")]
    [SerializeField] private Image Image_MainIcon;
    [SerializeField] private Text Text_Value1;
    [SerializeField] private Text Text_BuyPrice;
    [SerializeField] private GameObject GObj_Selected;
    [SerializeField] private DaniTechUIButton Button_SlotClick;

    private StdItemData _data;
    private Action<StdItemData> _onClickAction;

    private void OnEnable()
    {
        if (Button_SlotClick != null)
        {
            Button_SlotClick.BindOnClickButtonEvent(OnButtonClicked);
        }
    }

    private void OnDisable()
    {
        if (Button_SlotClick != null)
        {
            Button_SlotClick.UnBindOnClickButtonEvent(OnButtonClicked);
        }
    }

    public void InitSlot(StdItemData data, Action<StdItemData> onClickAction)
    {
        _data = data;
        _onClickAction = onClickAction;

        if (Text_Value1 != null) Text_Value1.text = data.Value1.ToString();
        if (Text_BuyPrice != null) Text_BuyPrice.text = data.BuyPrice.ToString();
        
        if (Image_MainIcon != null && !string.IsNullOrEmpty(data.IconPath))
        {
            DaniTechGameUtil.LoadAndSetSpriteImage(Image_MainIcon, data.IconPath).Forget();
        }
    }

    private void OnButtonClicked()
    {
        _onClickAction?.Invoke(_data);
    }

    public void SetSelectedUI(bool active)
    {
        if (GObj_Selected != null)
            GObj_Selected.SetActive(active);
    }
}