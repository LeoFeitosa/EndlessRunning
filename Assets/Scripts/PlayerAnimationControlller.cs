using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationControlller : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;
    private PlayerColision playerColision;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        playerColision = GetComponent<PlayerColision>();
    }

    void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRoll, player.IsRoll);
    }

    public void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }
}
