using System.Collections.Generic;
using UnityEngine;

public class StdBattleManager : MonoBehaviour
{
    private StdBattleUnitModel _playerUnit;
    private StdBattleUnitModel _enemyUnit;
    private List<StdBattleItemModel> _playerItems = new List<StdBattleItemModel>();

    private bool _isBattleActive = false;

    // 지속 데미지 체크를 위한 타이머
    private float _poisonTimer = 0f;
    private float _burnTimer = 0f;

    // 전투 시작 로직
    public void StartBattle(int playerHp, int enemyHp, List<StdItemData> playerItemDatas)
    {
        _playerUnit = new StdBattleUnitModel(playerHp);
        _enemyUnit = new StdBattleUnitModel(enemyHp);
        _playerItems.Clear();

        foreach (var data in playerItemDatas)
        {
            _playerItems.Add(new StdBattleItemModel(data));
        }

        _isBattleActive = true;
    }

    private void Update()
    {
        if (!_isBattleActive) return;

        // 1. 아이템 쿨타임 업데이트 및 발동 체크
        UpdateItemCoolDowns();
        // 스킬 연출 위치
        //        정보전달이 중요하다.
        //프리팹 동적생성 이펙트용
        //스킬사용시 코드에서는 이미 완료
        //이펙트용 캔버스에 루트안에서 연출 이펙트를 띄운다
        //카드의 공격시스템
        //스프라이트로 바꿔서 카드를
        //몬스터를 카드로 플레이어에게 투사체를 날리는 식으로
        //ai질문
        //오브젝트끼리 거리를 0이될때까지 스킬이펙트가 날아가는 로직


        // 2. 캐릭터의 디버프(독/발화) 데미지 계산
        UpdatePeriodicEffects();

        // 3. 전투 종료 판정
        CheckBattleEnd();
    }

    private void UpdateItemCoolDowns()
    {
        foreach (var item in _playerItems)
        {
            // 쿨타임이 0보다 클 때만 감소
            if (item.CurrentCoolDown > 0)
            {
                item.UpdateCoolDown(Time.deltaTime);
            }
            // 쿨타임이 0이 되면 효과 실행 및 0초 대기
            else if (item.IsReady)
            {
                ExecuteItemEffect(item, _enemyUnit);
                // 쿨타임 초기화 (4초면 다시 4초로)
                item.ResetCoolDown();
            }
        }
    }

    private void UpdatePeriodicEffects()
    {
        // 포이즌: 1초마다 피해
        _poisonTimer += Time.deltaTime;
        if (_poisonTimer >= 1.0f)
        {
            if (_enemyUnit.PoisonStack > 0)
            {
                _enemyUnit.TakePoisonDamage(_enemyUnit.PoisonStack);
                _enemyUnit.PoisonStack -= 1; // 틱당 1스택 차감
            }
            _poisonTimer = 0f;
        }

        // 발화: 0.5초마다 피해
        _burnTimer += Time.deltaTime;
        if (_burnTimer >= 0.5f)
        {
            if (_enemyUnit.BurnStack > 0)
            {
                // 쉴드 계산을 수행하고 막힌 양을 반환받음
                int blocked = _enemyUnit.TakeDamage(_enemyUnit.BurnStack);

                // 쉴드에 막혔으면 스택 절반(내림), 안 막혔으면 1 감소
                if (blocked > 0) _enemyUnit.BurnStack = Mathf.FloorToInt(_enemyUnit.BurnStack / 2f);
                else _enemyUnit.BurnStack -= 1;
            }
            _burnTimer = 0f;
        }
    }

    private void ExecuteItemEffect(StdBattleItemModel item, StdBattleUnitModel target)
    {
        //데미지 방식
        switch (item.Data.ValueType1)
        {
            case "Damage":
                target.TakeDamage(item.Data.Value1);
                break;
            case "Debuff_Poison":
                target.PoisonStack += item.Data.Value1;
                break;
            case "Debuff_Burn":
                target.BurnStack += item.Data.Value1;
                break;
        }
    }

    private void CheckBattleEnd()
    {
        if (!_playerUnit.IsDead && !_enemyUnit.IsDead) return;

        _isBattleActive = false;

        // 동시 패배 시 HP 비교 로직
        if (_playerUnit.IsDead && _enemyUnit.IsDead)
        {
            if (_playerUnit.CurrentHp < _enemyUnit.CurrentHp) Debug.Log("플레이어 패배");
            else Debug.Log("적 패배");
        }
        else if (_playerUnit.IsDead) Debug.Log("플레이어 패배");
        else Debug.Log("적 패배");
    }
}