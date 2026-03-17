using UnityEngine;

public class BossInputKeyTest : MonoBehaviour
{
    public Animator BossAnimator;
    private KeyCode EnterKey = KeyCode.F12;
    private KeyCode PatKey = KeyCode.F1;
    private KeyCode ThrowKey = KeyCode.F2;
    private KeyCode SweepKey = KeyCode.F3;


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

    }
}
