using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationControlller : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
    }
}
