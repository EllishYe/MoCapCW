using UnityEngine;

/// <summary>
/// Boss血量和阶段管理，负责处理Boss的HP、阶段转换以及根据HP触发的事件
/// </summary>
public class BossHealth : MonoBehaviour
{
    public float maxHealth = 600f;
    public float currentHealth;

    public BossPhase phase = BossPhase.Phase1;

    [SerializeField] HealthBar bossHealthBar;

    //public BossState currentState;

    //private BossStateController stateController;

    

    void Start()
    {
        currentHealth = maxHealth;

        if (bossHealthBar != null)
        {
            bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        else {
            Debug.LogWarning("BossHealthBar ref is missing");
        }
        
        //stateController = GetComponent<BossStateController>();
    }

    public bool TakeDamage(float damage, BossBodyPartType part)
    {
        // Phase1: Can only take damage on RightLowerArm
        if (phase == BossPhase.Phase1)
        {
            if (part != BossBodyPartType.RightLowerArm)
                return false;
        }

        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth);
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        //CheckPhase1Events();
        return true;
    }

    //void CheckPhase1Events()
    //{
    //    float hpRatio = currentHealth / maxHealth;

    //    // 🔴 5/6 → Hurt + Stun
    //    if (hpRatio <= 5f / 6f && currentState == BossState.Attack)
    //    {
    //        stateController.TriggerHurt();
    //    }

    //    // 🔴 4/6 → ArmFall（任何状态都可以触发）
    //    if (hpRatio <= 4f / 6f)
    //    {
    //        stateController.TriggerArmFall();
    //    }
    //}
}
