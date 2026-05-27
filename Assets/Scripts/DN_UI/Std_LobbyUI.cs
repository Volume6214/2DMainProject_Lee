using UnityEngine;

public class Std_LobbyUI :DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Button_StartGame;
    [SerializeField] private DaniTechUIButton Button_QuitGame;

    private void OnEnable()
    {
        Button_StartGame.BindOnClickButtonEvent(Onclick_StartGame);
        Button_StartGame.BindOnClickButtonEvent(Onclick_QuitGame);
    }

    private void Onclick_StartGame()
    {
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StdLobbyUI);
    }

    private void Onclick_QuitGame()
    {
    }
}
