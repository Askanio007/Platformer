using AloneCrew;
using UnityEngine;

public class BossBigShootState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage = 1;
        spawner.LaunchProjectile();
    }
}
