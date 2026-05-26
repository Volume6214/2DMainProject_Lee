using UnityEngine;

public enum Std_2DEntityAnimState
{
    None = 0,
    Idle,
    Walk,
    Atk,
    Skill,
    Dead
}

public class Std_2DAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator Animator_Entity;

    private Std_2DEntityAnimState _currentAnimState;

    public void SetState(Std_2DEntityAnimState newState)
    {
        if (newState == Std_2DEntityAnimState.Idle && _currentAnimState == Std_2DEntityAnimState.Idle)
        {
            return;
        }

        _currentAnimState = newState;

        switch (_currentAnimState)
        {
            case Std_2DEntityAnimState.Idle:
                ResetAllAnimParameters();
                break;
            case Std_2DEntityAnimState.Walk:
                Animator_Entity.SetBool("IsWalk", true);
                break;
            case Std_2DEntityAnimState.Atk:
                Animator_Entity.SetTrigger("IsAtk");
                break;
            case Std_2DEntityAnimState.Skill:
                 Animator_Entity.SetTrigger("IsSkill");
                break;
            case Std_2DEntityAnimState.Dead:
                Animator_Entity.SetTrigger("IsDead");
                break;
            default:
                ResetAllAnimParameters();
                break;
        }
    }

    private void ResetAllAnimParameters()
    {
        Animator_Entity.SetBool("IsWalk", false);
    }
}
