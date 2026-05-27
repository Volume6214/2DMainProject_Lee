using UnityEngine;

public class Std_MainUITemp : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Button_StashInventory;
    [SerializeField] private DaniTechUIButton Button_Close_StashInventory;

    private void OnEnable()
    {
        Button_StashInventory.BindOnClickButtonEvent(Onclick_StashInventory);
        Button_Close_StashInventory.BindOnClickButtonEvent(Onclick_Close_StashInventory);
    }

    private void Onclick_StashInventory()
    {
        DaniTechUIManager.Instance.OpenContentUI(DaniTechUIType.StashInventoryUI);
    }

    private void Onclick_Close_StashInventory()
    {
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StashInventoryUI);
    }
}
