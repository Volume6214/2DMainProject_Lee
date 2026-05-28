using UnityEngine;

public class Std_MainUITemp : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Button_StashInventory;
    [SerializeField] private DaniTechUIButton Button_Close_StashInventory;
    [SerializeField] private GameObject StashInventory;

    private void OnEnable()
    {
        Button_StashInventory.BindOnClickButtonEvent(Onclick_StashInventory);
        Button_Close_StashInventory.BindOnClickButtonEvent(Onclick_CloseStashInventory);
    }

    private void Onclick_StashInventory()
    {
        //DaniTechUIManager.Instance.OpenContentUI(DaniTechUIType.StashInventoryUI);
        StashInventory.SetActive(true);
    }

    private void Onclick_CloseStashInventory()
    {
        //DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StashInventoryUI);
        StashInventory.SetActive(false);
    }
}
