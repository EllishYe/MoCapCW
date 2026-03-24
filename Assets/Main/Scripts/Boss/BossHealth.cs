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

    [Header("References")]
    public BossAI bossAI;

    //public BossState currentState;

    //private BossStateController stateController;

    // Blood Control
    private bool hasTriggeredHurt = false;
    private bool hasTriggeredArmFall = false;

    bool isInvulnerable = false;

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
        if (isInvulnerable)
        {
            return false; 
        }

        // Phase1: Can only take damage on RightLowerArm
        if (phase == BossPhase.Phase1)
        {
            if (part != BossBodyPartType.RightLowerArm)
                return false;
        }

        currentHealth -= damage;
        Debug.Log("Boss HP: " + currentHealth);
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        CheckPhase1Events();
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

    public void EnterPhase(BossPhase newPhase)
    {
        phase = newPhase;

        // reset phase-specific tirgger flags
        hasTriggeredHurt = false;
        hasTriggeredArmFall = false;

        Debug.Log("Enter " + newPhase);
    }

    public void EnterInvulnerable() {
        isInvulnerable = true;
        Debug.Log("Enter Invulnerable");
    }
    public void ExitInvulnerable()
    {
        isInvulnerable = false;
        Debug.Log("Exit Invulnerable");
    }

}
