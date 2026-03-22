using UnityEngine;

public class BossInput : MonoBehaviour
{
    [Header("Reference")]
    public BossAI bossAI;

    [Header("Keys")]
    private KeyCode EnterKey = KeyCode.F12;

    private KeyCode PatKey = KeyCode.F1;
    private KeyCode ThrowKey = KeyCode.F2;
    private KeyCode SweepKey = KeyCode.F3;
    private KeyCode HurtKey = KeyCode.F4;
    private KeyCode ArmFallLKey = KeyCode.F5;
    private KeyCode ArmFallRKey = KeyCode.F6;
    private KeyCode DieKey = KeyCode.F7;

    void Update()
    {
        if (bossAI == null) return;

        if (Input.GetKeyDown(EnterKey))
        {
            bossAI.EnterBoss();
        }

        if (Input.GetKeyDown(PatKey))
        {
            bossAI.PlayPat();
        }

        if (Input.GetKeyDown(ThrowKey))
        {
            bossAI.PlayThrow();
        }

        if (Input.GetKeyDown(SweepKey))
        {
            bossAI.PlaySweep();
        }

        if (Input.GetKeyDown(HurtKey))
        {
            bossAI.TriggerHurt();
        }

        if (Input.GetKeyDown(ArmFallLKey))
        {
            bossAI.PlayArmFallL();
        }

        if (Input.GetKeyDown(ArmFallRKey))
        {
            bossAI.PlayArmFallR();
        }

        if (Input.GetKeyDown(DieKey))
        {
            bossAI.TriggerDie();
        }
    }
}