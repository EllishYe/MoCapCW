using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss血量和阶段管理，负责处理Boss的HP、阶段转换以及根据HP触发的事件
/// </summary>
public class BossHealth : MonoBehaviour
{
    public float maxHealth = 900f;
    public float currentHealth;

    public BossPhase phase = BossPhase.Phase1;

    [SerializeField] HealthBar bossHealthBar;

    [Header("References")]
    public BossAI bossAI;


    // Blood Control
    private bool hasTriggeredHurt = false;
    private bool hasTriggeredArmFall = false;

    bool isInvulnerable = false;

    // Phase-specific valid body parts
    private List<BossBodyPartType> validParts = new List<BossBodyPartType>();

    void Start()
    {
        currentHealth = maxHealth;
        UpdateValidBodyParts();

        if (bossHealthBar != null)
        {
            bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        else {
            Debug.LogWarning("BossHealthBar ref is missing");
        }
        
    }

    public bool TakeDamage(float damage, BossBodyPartType part)
    {
        if (isInvulnerable)
        {
            return false; 
        }

        // No harm senario
        if (validParts.Count > 0 && !validParts.Contains(part))
        {
            return false;
        }

        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth);
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        // Check Phase Events
        switch (phase)
        {
            case BossPhase.Phase1:
                CheckPhase1Events();
                break;

            case BossPhase.Phase2:
                CheckPhase2Events();
                break;

            case BossPhase.Phase3:
                CheckPhase3Events();
                break;
        }

        return true;
    }

    void CheckPhase1Events()
    {
        float hpRatio = currentHealth / maxHealth;

        // 5/6-> Hurt 阶段内事件，触发一次
        if (!hasTriggeredHurt&&hpRatio <= 5f / 6f )
        {
            hasTriggeredHurt = true;
            bossAI.TriggerHurt();
            Debug.Log("5/6 Boss hurt");
        }

        // 4/6 → ArmFall 阶段内的事件，触发一次
        if (!hasTriggeredArmFall&&hpRatio <= 4f / 6f)
        {
            hasTriggeredArmFall = true;
            bossAI.PlayArmFallR();
            Debug.Log("4/6 Boss ArmfallR");
        }
    }
    void CheckPhase2Events()
    {
        float hpRatio = currentHealth / maxHealth;

        // 3/6-> Hurt 阶段内事件，触发一次
        if (!hasTriggeredHurt && hpRatio <= 3f / 6f)
        {
            hasTriggeredHurt = true;
            bossAI.TriggerHurt();
            Debug.Log("3/6 Boss hurt");
        }

        // 2/6 → ArmFall 阶段内的事件，触发一次
        if (!hasTriggeredArmFall && hpRatio <= 2f / 6f)
        {
            hasTriggeredArmFall = true;
            bossAI.PlayArmFallR();
            Debug.Log("2/6 Boss ArmfallR");
        }
    }

    void CheckPhase3Events() { 
        // Phase3对应的事件
    }



    public void EnterNextPhase()
    {
        BossPhase nextPhase = phase;

        switch (phase)
        {
            case BossPhase.Phase1:
                nextPhase = BossPhase.Phase2;
                break;

            case BossPhase.Phase2:
                nextPhase = BossPhase.Phase3;
                break;

            case BossPhase.Phase3:
                return;
        }

        phase = nextPhase;
        Debug.Log("Enter " + phase);
        
        UpdateValidBodyParts();

        // reset phase-specific trigger flags
        hasTriggeredHurt = false;
        hasTriggeredArmFall = false;

        if (bossAI != null)
        {
            //bossAI.OnPhaseChanged(phase); Pat攻击动作变化/Pat攻击动作取消
        }
    }

    void UpdateValidBodyParts()
    {
        validParts.Clear();

        switch (phase)
        {
            case BossPhase.Phase1:
                validParts.Add(BossBodyPartType.RightLowerArm);
                break;

            case BossPhase.Phase2:
                validParts.Add(BossBodyPartType.LeftLowerArm);
                break;

            case BossPhase.Phase3:
                // Phase3 默认不可攻击（靠按钮）
                break;
        }
    }


    #region Invulnerable Control
    public void EnterInvulnerable() {
        isInvulnerable = true;
        Debug.Log("Enter Invulnerable");
    }
    public void ExitInvulnerable()
    {
        isInvulnerable = false;
        Debug.Log("Exit Invulnerable");
    }
    #endregion
}
