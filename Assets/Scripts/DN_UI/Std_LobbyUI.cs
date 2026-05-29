using UnityEngine;

public class Std_LobbyUI :DaniTechUIBase
{
    [Header("로비버튼영역")]
    [SerializeField] private DaniTechUIButton Button_StartGame;
    [SerializeField] private DaniTechUIButton Button_QuitGame;
    [SerializeField] private DaniTechUIButton Button_ItemInfo;
    //[SerializeField] private DaniTechUIButton Button_CloseItemInfo;
     
    [Header("도감영역")]
    [SerializeField] private GameObject StdItemInfoUI;

    private void OnEnable()
    {
        Button_StartGame.BindOnClickButtonEvent(Onclick_StartGame);
        Button_QuitGame.BindOnClickButtonEvent(Onclick_QuitGame);
        Button_ItemInfo.BindOnClickButtonEvent(Onclick_ItemInfoUI);
        //Button_CloseItemInfo.BindOnClickButtonEvent(Onclick_CloseItemInfo);
    }

    private void Onclick_StartGame()
    {
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StdLobbyUI);
    }

    private void Onclick_QuitGame()
    {
    }

    private void Onclick_ItemInfoUI()
    {
        DaniTechUIManager.Instance.OpenContentUI(DaniTechUIType.Std_ItemInfoUI);
    }

    private void Onclick_CloseItemInfo()
    {
        StdItemInfoUI.SetActive(false);
    }
}
