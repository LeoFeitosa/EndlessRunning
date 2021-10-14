using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerController player;
    [SerializeField] PlayerAnimationController playerAnimationController;

    [Header("UI")]
    [SerializeField] MainHUD mainHUD;

    [SerializeField] MusicPlayer musicPlayer;

    [Header("Gameplay")]
    [SerializeField] private float startPlayerSpeed = 10;
    [SerializeField] private float maxPlayerSpeed = 20;
    [SerializeField] private float timeToMaxSpeedSeconds = 5 * 60;

    [SerializeField] private float reloadGameDelay = 3;

    [SerializeField]
    [Range(0, 5)]
    private int startGameCountdown = 5;

    [Header("Score")]
    [SerializeField] private int baseScoreMultiplier = 1;

    private float score;
    public int Score => Mathf.RoundToInt(score);

    private float startGameTime;
    private bool isGameRunning = false;

    void Awake()
    {
        SetWaitForStartGameState();
    }

    void Update()
    {
        if (isGameRunning)
        {
            float timePercent = (Time.time - startGameTime) / timeToMaxSpeedSeconds;
            player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxPlayerSpeed, timePercent);
            float extraScoreMultiplier = 1 + timePercent;
            score += baseScoreMultiplier * extraScoreMultiplier * player.ForwardSpeed * Time.deltaTime;
        }
    }

    private void SetWaitForStartGameState()
    {
        player.enabled = false;
        mainHUD.ShowStartGameOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        isGameRunning = false;
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(1);
        musicPlayer.PlayGameOverMusic();
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator StartGameCor()
    {
        musicPlayer.PlayMainTrackMusic();
        yield return StartCoroutine(mainHUD.PlayStartGameCountdown(startGameCountdown));
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());

        player.enabled = true;
        player.ForwardSpeed = startPlayerSpeed;
        startGameTime = Time.time;
        isGameRunning = true;
    }
}
