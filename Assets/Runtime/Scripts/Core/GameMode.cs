using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] PlayerAnimationController playerAnimationController;
    [SerializeField] MainHUD mainHUD;
    [SerializeField] MusicPlayer musicPlayer;
    [SerializeField] private float reloadGameDelay = 3;
    [SerializeField]
    [Range(0, 5)]
    private int startGameCountdown = 5;

    void Awake()
    {
        SetWaitForStartGameState();
    }

    private void SetWaitForStartGameState()
    {
        player.enabled = false;
        mainHUD.ShowStartGameOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
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
        playerAnimationController.PlayStartGameAnimation();
    }
}
