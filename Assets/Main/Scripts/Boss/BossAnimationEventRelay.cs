using UnityEngine;

public class BossAnimationEventRelay : MonoBehaviour
{
    public BossAI bossAI;
    public BossAttackController bossAttackController;
    public BossHealth bossHealth;
    public ArmDisapper armDisapper;

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
    public void OnDie()
    {
        bossAI.OnDie();
    }


    #region Animation Events for Pat

    public void OnPatHitLeft() {
        bossAttackController.OnPatHit(0);
    }
    public void OnPatHitRight()
    {
        bossAttackController.OnPatHit(1);
    }


    #endregion

    #region IsInvulnerable

    public void OnArmfallRightStart()
    {
        bossHealth.EnterInvulnerable();
    }
    public void OnArmfallRightFinished()
    {
        bossHealth.ExitInvulnerable();
        bossHealth.EnterNextPhase();
    }
    public void OnArmfallLeftStart()
    {
        bossHealth.EnterInvulnerable();
    }
    public void OnArmfallLeftFinished()
    {
        bossHealth.ExitInvulnerable();
        bossHealth.EnterNextPhase();
    }

    #endregion

    #region Arm Disappear

    public void RightArmDisappear()
    {
        armDisapper.PlayDisappearRight();
        bossAttackController.OnLeftArmDisappear();
    }
    public void LeftArmDisappear()
    {
        armDisapper.PlayDisappearLeft();
        bossAttackController.OnRightArmDisappear();
    }

    #endregion

    #region Animation Events for Sweep

    public void OnSweepHit() {
        bossAttackController.OnSweepHit();
    }

    #endregion

    #region Animation Events for Throw


    public void OnSpawnBall()
    {
        bossAttackController.SpawnEnergyBall();
    }
    public void OnThrowBall()
    {
        bossAttackController.ThrowEnergyBall();
    }

    #endregion

}
