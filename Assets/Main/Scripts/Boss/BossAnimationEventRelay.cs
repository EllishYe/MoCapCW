using UnityEngine;

public class BossAnimationEventRelay : MonoBehaviour
{
    public BossAI bossAI;

    public void OnEnterFinished()
    {
        bossAI.OnEnterFinished();
    }

    public void OnAttackFinished()
    {
        bossAI.OnAttackFinished();
    }

    public void OnInterrupted()
    {
        bossAI.OnInterrupted();
    }

    public void OnInterruptFinished()
    {
        bossAI.OnInterruptFinished();
    }
    
}
