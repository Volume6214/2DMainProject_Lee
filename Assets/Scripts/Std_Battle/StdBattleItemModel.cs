public class StdBattleItemModel
{
    public StdItemData Data { get; private set; }
    public float CurrentCoolDown { get; private set; }
    public int CurrentStack { get; set; }

    public StdBattleItemModel(StdItemData data)
    {
        Data = data;
        CurrentCoolDown = data.CoolDown; // 시작 시 초기 쿨타임
    }

    public void UpdateCoolDown(float deltaTime)
    {
        if (CurrentCoolDown > 0)
        {
            CurrentCoolDown -= deltaTime;
            if (CurrentCoolDown < 0) CurrentCoolDown = 0; // 0에서 멈춤
        }
    }

    public void ResetCoolDown()
    {
        CurrentCoolDown = Data.CoolDown; 
    }
    public bool IsReady => CurrentCoolDown <= 0;
}