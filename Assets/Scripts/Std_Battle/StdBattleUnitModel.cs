using UnityEngine;

public class StdBattleUnitModel
{
    public int MaxHp { get; set; }
    public int CurrentHp { get; set; }
    public int CurrentShield { get; set; }

    // 유닛이 스스로 관리하는 상태
    public int PoisonStack { get; set; }
    public int BurnStack { get; set; }
    public int RecoveryStadck { get; set; }

    public bool IsDead => CurrentHp <= 0;

    public StdBattleUnitModel(int hp)
    {
        MaxHp = hp;
        CurrentHp = hp;
    }

    // 데미지 처리 (발화 포함)
    public int TakeDamage(int amount)
    {
        int blocked = 0;
        if (CurrentShield > 0)
        {
            blocked = Mathf.Min(CurrentShield, amount);
            CurrentShield -= blocked;
            amount -= blocked;
        }
        CurrentHp -= amount;
        return blocked;
    }

    // 쉴드 무시 데미지
    public void TakePoisonDamage(int amount) => CurrentHp -= amount;
}