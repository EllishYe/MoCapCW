using UnityEngine;

public class BossInputKeyTest : MonoBehaviour
{
    public Animator BossAnimator;
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
        if (Input.GetKeyDown(EnterKey))
        {
            BossAnimator.SetBool("IsEntered", true);
        }
        if (Input.GetKeyDown(PatKey))
        {
            BossAnimator.SetTrigger("PatTrigger");
        }
        if (Input.GetKeyDown(ThrowKey))
        {
            BossAnimator.SetTrigger("ThrowTrigger");
        }
        if (Input.GetKeyDown(SweepKey))
        {
            BossAnimator.SetTrigger("SweepTrigger");
        }
        if (Input.GetKeyDown(HurtKey))
        {
            BossAnimator.SetTrigger("HurtTrigger");
        }
        if (Input.GetKeyDown(ArmFallLKey))
        {
            BossAnimator.SetTrigger("ArmFallLTrigger");
        }
        if (Input.GetKeyDown(ArmFallRKey))
        {
            BossAnimator.SetTrigger("ArmFallRTrigger");
        }
        if (Input.GetKeyDown(DieKey))
        {
            BossAnimator.SetTrigger("DieTrigger");
        }

    }
}
