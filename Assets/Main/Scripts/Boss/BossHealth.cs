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
    [Header("Phase3 Buttons")]
    public BtnMove buttonA;
    public BtnMove buttonB;
    public BtnMove buttonC;

    // Blood Control
    private bool hasTriggeredHurt = false;
    private bool hasTriggeredArmFall = false;

    bool isInvulnerable = false;

    // Phase-specific valid body parts
    private List<BossBodyPartType> validParts = new List<BossBodyPartType>();

    // Phase3 Stun Control
    //private bool isPhase3Stunned = false;
    private int phase3Stage = 0; // 0=A, 1=B, 2=C
    public ScreenStateController screenController;

    //void Update()
    //{
    //    if (phase == BossPhase.Phase3)
    //    {
    //        HandlePhase3Input();
    //    }
    //}

    //void HandlePhase3Input()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        TriggerButtonA();
    //    }

    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        TriggerButtonB();
    //    }

    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        TriggerButtonC();
    //    }
    //}


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

        if (phase == BossPhase.Phase3)
            return false;
        
        if (isInvulnerable)
        {
            return false; 
        }

        // no harm if hit invalid part
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
            bossAI.PlayArmFallL();
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
                InitPhase3();
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


    #region Phase3 Control

    public void TriggerButtonA()
    {
        //Button A Btn press event
        if (phase3Stage != 0) return;

        Debug.Log("Button A triggered");
        
        bossAI.TriggerHurt();

        // 血量直接压到 2/9
        currentHealth = maxHealth * (2f / 9f);
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        // 电视机关一组
        screenController.ToggleGroup(0);

        // 切换按钮（可能要写逻辑）
        phase3Stage = 1;
        if (buttonB != null)
        {
            buttonB.gameObject.SetActive(true);
            buttonB.PlayMove();
        }

    }

    public void TriggerButtonB()
    {
        if (phase3Stage != 1) return;

        Debug.Log("Button B triggered");
        
        bossAI.TriggerHurt();

        currentHealth = maxHealth * (1f / 9f);
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        screenController.ToggleGroup(1);

        phase3Stage = 2;
        if (buttonC != null)
        {
            buttonC.gameObject.SetActive(true);
            buttonC.PlayMove();
        }
    }

    public void TriggerButtonC()
    {
        if (phase3Stage != 2) return;
        Debug.Log("Button C triggered");
        bossAI.TriggerDie();

        currentHealth = 0f;
        bossHealthBar.UpdateHealthBar(currentHealth, maxHealth);

        screenController.ToggleGroup(2);

        phase3Stage = 3;
    }

    void InitPhase3()
    {
        phase3Stage = 0;

        // 只让 A 出现
        if (buttonA != null)
            buttonA.PlayMove();

        if (buttonB != null)
            buttonB.gameObject.SetActive(false);

        if (buttonC != null)
            buttonC.gameObject.SetActive(false);
    }

    #endregion
}
