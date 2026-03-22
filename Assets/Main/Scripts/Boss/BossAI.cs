using System.Collections;
using UnityEngine;

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
            if (isInterrupted) continue;

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

        // 同时触发两个Animator
        lowerAnimator.SetTrigger(triggerName);
        upperAnimator.SetTrigger(triggerName);
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
    }

    /// <summary>
    /// 打断结束（Hurt / ArmFall结束时调用）
    /// </summary>
    public void OnInterruptFinished()
    {
        isInterrupted = false;
    }

    /// <summary>
    /// Boss死亡（可扩展）
    /// </summary>
    public void OnDie()
    {
        isAlive = false;
        StopAllCoroutines();
    }
}
