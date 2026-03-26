using UnityEngine;

public class ThrowStateBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossAttackController controller = animator.GetComponent<BossAttackController>();
        if (controller != null)
        {
            controller.CancelCurrentBall();
        }
    }
}
