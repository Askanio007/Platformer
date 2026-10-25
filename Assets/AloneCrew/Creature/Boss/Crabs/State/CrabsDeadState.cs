using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public class CrabsDeadState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var launcher = animator.GetComponent<MultiplyProjectileSpawner>();
        var verticalLauncher = animator.GetComponent<VerticalProjectileSpawner>();
        launcher.Stop();
        verticalLauncher.Stop();
        animator.SetBool("IsCast", false);
    }
}
