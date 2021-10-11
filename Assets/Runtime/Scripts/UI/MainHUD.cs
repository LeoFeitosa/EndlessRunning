using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject startGameOverlay;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;
    private MainHUDAudioController audioController;

    void Awake()
    {
        ShowHudOverlay();
        audioController = GetComponent<MainHUDAudioController>();
    }

    void LateUpdate()
    {
        scoreText.text = $"Score : {player.Score}";
        distanceText.text = $"Score : {Mathf.RoundToInt(player.TravelledDistance)}m";
    }

    public void StartGame()
    {
        gameMode.StartGame();
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

    public void ShowStartGameOverlay()
    {
        startGameOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
    }

    void ShowHudOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
    }

    void ShowPauseOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
    }

    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowHudOverlay();
        countdownText.gameObject.SetActive(false);

        if (countdownSeconds == 0)
        {
            yield break;
        }

        float timeToStart = Time.time + countdownSeconds;
        yield return null;
        countdownText.gameObject.SetActive(true);
        int previousRemainingTimeInt = countdownSeconds;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remainingTimeInt + 1).ToString();

            //tocar o audio quando o numero trocar
            if (previousRemainingTimeInt != remainingTimeInt)
            {
                audioController.PlayCountdownSound();
            }
            previousRemainingTimeInt = remainingTimeInt;

            //animacao
            float percent = remainingTime - remainingTimeInt;
            countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        audioController.PlayCountdownFinishedSound();

        countdownText.gameObject.SetActive(false);
    }
}
