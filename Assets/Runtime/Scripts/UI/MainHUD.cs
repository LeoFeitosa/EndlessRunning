using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;

    void LateUpdate()
    {
        scoreText.text = $"Score : {player.Score}";
    }
}
