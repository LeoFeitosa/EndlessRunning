using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTags
{
    public const string Obstacle = "Obstacle";
}

public class PlayerColision : MonoBehaviour
{
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
        }
    }
}
