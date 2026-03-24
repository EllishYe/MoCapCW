using UnityEngine;


/// <summary>
/// Boss状态控制器，负责根据BossHealth的事件触发动画状态转换，并提供外部接口供BossHealth调用
/// </summary>
public class BossStateController : MonoBehaviour
{
    public Animator animator;

    public BossState currentState;

    public void TriggerHurt()
    {
        if (currentState == BossState.Hurt || currentState == BossState.Stun)
            return;

        currentState = BossState.Hurt;
        animator.SetTrigger("Hurt");
    }

    public void EnterStun()
    {
        currentState = BossState.Stun;
        animator.SetBool("IsStun", true);
    }

    public void ExitStun()
    {
        currentState = BossState.Attack;
        animator.SetBool("IsStun", false);
    }

    public void TriggerArmFall()
    {
        if (currentState == BossState.ArmFall)
            return;

        currentState = BossState.ArmFall;
        animator.SetTrigger("ArmFall");
    }
}
