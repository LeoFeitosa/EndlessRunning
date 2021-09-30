using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    private PlayerController playerController;
    private PlayerAnimationControlller animationControlller;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationControlller = GetComponent<PlayerAnimationControlller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.Obstacle))
        {
            Debug.Log("Com tag");
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            playerController.Die();
            animationControlller.Die();
            gameMode.OnGameOver();
        }
    }
}
