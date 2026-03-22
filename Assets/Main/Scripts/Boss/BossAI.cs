using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public BossAttackQueue attackQueue;

    [Header("Settings")]
    public float attackCooldown = 1.5f;

    private bool isAttacking = false;
    private bool isInterrupted = false;
    private bool isAlive = true;

    void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            // 等待可以攻击
            yield return new WaitUntil(() => !isAttacking && !isInterrupted);

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
        switch (type)
        {
            case BossAttackQueue.AttackType.Pat:
                animator.SetTrigger("PatTrigger");
                break;

            case BossAttackQueue.AttackType.Throw:
                animator.SetTrigger("ThrowTrigger");
                break;

            case BossAttackQueue.AttackType.Sweep:
                animator.SetTrigger("SweepTrigger");
                break;
        }
    }

    // ===== Animation Events =====

    /// <summary>
    /// 攻击动画结束（在动画最后一帧调用）
    /// </summary>
    public void OnAttackFinished()
    {
        isAttacking = false;
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
