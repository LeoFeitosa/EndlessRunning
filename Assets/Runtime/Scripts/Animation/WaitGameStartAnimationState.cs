using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitGameStartAnimationState : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //corriginr isso no futuro
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
        }
    }
}

