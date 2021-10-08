using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;


    void Awake()
    {
        ShowHudOverlay();
    }

    void LateUpdate()
    {
        scoreText.text = $"Score : {player.Score}";
        distanceText.text = $"Score : {Mathf.RoundToInt(player.TravelledDistance)}m";
    }

    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    }

    void ShowHudOverlay()
    {
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
    }

    void ShowPauseOverlay()
    {
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
    }
}
