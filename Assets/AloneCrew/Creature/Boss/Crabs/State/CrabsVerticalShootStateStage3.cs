using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public class CrabsVerticalShootStateStage3 : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var launcher = animator.GetComponent<VerticalProjectileSpawner>();
        launcher.LaunchProjectile();
    }
}
