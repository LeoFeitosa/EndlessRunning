using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationControlller : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;
    private PlayerCollision playerCollision;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }

    public void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }
}
