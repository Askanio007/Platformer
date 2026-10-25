using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public abstract class AbstractCrabsShootState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsCast", true);
        var launcher = animator.GetComponent<AbstractProjectileSpawner>();
        UpdateStage(launcher);
        launcher.LaunchProjectile(() => animator.SetBool("IsCast", false));
    }
    
    protected abstract void UpdateStage(AbstractProjectileSpawner spawner);
}
