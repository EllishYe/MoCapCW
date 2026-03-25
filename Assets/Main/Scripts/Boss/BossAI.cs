using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 动作指令AI，负责根据状态机和攻击队列控制Boss的攻击节奏和动画触发
/// </summary>
public class BossAI : MonoBehaviour
{
    [Header("References")]
    public Animator lowerAnimator; // Figure01
    public Animator upperAnimator; // Figure02
    public BossAttackQueue attackQueue;

    [Header("Settings")]
    public float attackCooldown = 1.5f;

    private bool isAttacking = false;
    private bool isInterrupted = false;
    private bool isAlive = true;
    bool canAttack = false;

    void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            
            // 等待可以攻击
            yield return new WaitUntil(() => canAttack && !isAttacking && !isInterrupted);

            // 冷却时间
            yield return new WaitForSeconds(attackCooldown);

            // 再次确认（防止中途被打断）
            if (isInterrupted)
            {
                continue;
            }

            // 获取攻击
            var attack = attackQueue.GetNextAttack();

            // 执行攻击
            PlayAttack(attack);

            isAttacking = true;
        }
    }

    void PlayAttack(BossAttackQueue.AttackType type)
    {
        string triggerName = "";

        switch (type)
        {
            case BossAttackQueue.AttackType.Pat:
                triggerName = "PatTrigger";
                break;

            case BossAttackQueue.AttackType.Throw:
                triggerName = "ThrowTrigger";
                break;

            case BossAttackQueue.AttackType.Sweep:
                triggerName = "SweepTrigger";
                break;
        }
        //输出本次要执行的攻击信息到 Console
        Debug.Log($"[BossAI] AttackType={type} ");

        // 同时触发两个Animator
        lowerAnimator?.SetTrigger(triggerName);
        upperAnimator?.SetTrigger(triggerName);
    }

    // ===== Animation Events =====

    /// <summary>
    /// 入场动画结束
    /// </summary>
    public void OnEnterFinished()
    {
        canAttack = true;
    }

    /// <summary>
    /// 攻击动画结束（在动画最后一帧调用）
    /// </summary>
    public void OnAttackFinished()
    {
        isAttacking = false;
        Debug.Log("Attack Finished");
    }

    /// <summary>
    /// 被打断（进入Hurt / ArmFall时调用）
    /// </summary>
    public void OnInterrupted()
    {
        isInterrupted = true;
        isAttacking = false;
        canAttack = false;
        Debug.Log("Boss Interrupted");
    }

    /// <summary>
    /// 打断结束（Hurt / ArmFall结束时调用）
    /// </summary>
    public void OnInterruptFinished()
    {
        isInterrupted = false;
        canAttack = true;
        Debug.Log("Boss Interrupt Finished");
    }

    /// <summary>
    /// Boss死亡（可扩展）
    /// </summary>
    public void OnDie()
    {
        //isAlive = false;
        //StopAllCoroutines();
        //进入下一个场景Outro
        SceneManager.LoadScene("Outro");
    }

    #region Tigger Update
    public void EnterBoss()
    {
        lowerAnimator.SetBool("IsEntered", true);
        upperAnimator.SetBool("IsEntered", true);
    }

    public void PlayPat()
    {
        lowerAnimator.SetTrigger("PatTrigger");
        upperAnimator.SetTrigger("PatTrigger");
    }

    public void PlayThrow()
    {
        lowerAnimator.SetTrigger("ThrowTrigger");
        upperAnimator.SetTrigger("ThrowTrigger");
    }

    public void PlaySweep()
    {
        lowerAnimator.SetTrigger("SweepTrigger");
        upperAnimator.SetTrigger("SweepTrigger");
    }

    public void TriggerHurt()
    {
        lowerAnimator.SetTrigger("HurtTrigger");
        upperAnimator.SetTrigger("HurtTrigger");
    }

    public void PlayArmFallL()
    {
        lowerAnimator.SetTrigger("ArmFallLTrigger");
        upperAnimator.SetTrigger("ArmFallLTrigger");
    }

    public void PlayArmFallR()
    {
        lowerAnimator.SetTrigger("ArmFallRTrigger");
        upperAnimator.SetTrigger("ArmFallRTrigger");
    }

    public void TriggerDie()
    {
        lowerAnimator.SetTrigger("DieTrigger");
        upperAnimator.SetTrigger("DieTrigger");
    }
    #endregion


}
