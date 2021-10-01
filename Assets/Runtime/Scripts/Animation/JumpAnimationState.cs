using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //olha a duração de animação do pulo
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];
            //ola a duração do pulo no gameplay
            //arrumar depois
            PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(PlayerAnimationConstants.JumpMultiplier, multiplier);
        }
    }
}