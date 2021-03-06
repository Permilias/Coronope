﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DifficultyConfig difficultyConfig;

    float baseSpeedMult;
    public float speedMultiplier;
    public float minSpeed;

    float addedSpeed;

    private void Awake()
    {
        Instance = this;

        baseSpeedMult = speedMultiplier;
        speedMultiplier = 0f;

        addedSpeed = (difficultyConfig.maxAcceleration - baseSpeedMult) / difficultyConfig.accelerationGrowingDuration;
    }

    private void Start()
    {
        MusicManager.Instance.Initialize();
        FXPlayer.Instance.Initialize();
        CollectibleManager.Instance.Initialize();

        InfectionManager.Instance.Initialize();
        SneezingManager.Instance.Initialize();

        DecorumManager.Instance.Initialize();

        ChunkManager.Instance.Initialize();

        PlayerAnimation.Instance.Initialize();
        PlayerController.Instance.RefreshMovementValues();

        TutorialManager.Instance.Initialize();

        StartMenu();


    }

    float difficultyCount;
    private void Update()
    {
        if (speedMultiplier == 0) return;

        if (ChunkManager.Instance.inTutorial) return;

        //grow difficulty
        difficultyCount += Time.deltaTime;
        if(difficultyCount >= 1f)
        {
            difficultyCount -= 1f;
            speedMultiplier += addedSpeed;

            if (speedMultiplier >= difficultyConfig.maxAcceleration) speedMultiplier = difficultyConfig.maxAcceleration;
        }
    }

    public void ResetSpeed()
    {
        difficultyCount = 0f;
        DOTween.To(() => speedMultiplier, x => speedMultiplier = x, baseSpeedMult, 0.5f);
    }

    public void StartGame()
    {
        gameOngoing = true;
        speedMultiplier = baseSpeedMult;
        CameraAnimator.Instance.SetGameplay();
        PlayerAnimation.Instance.TurnToStreet();
        CanvasAnim_Game.Instance.Display();
        CanvasAnim_StartMenu.Instance.Hide();
    }

    public void GoToMenu()
    {
        gameOngoing = false;
        speedMultiplier = 0;
        CameraAnimator.Instance.SetCinematic();
        PlayerAnimation.Instance.TurnToCamera();

    }

    public void StartMenu()
    {
        CanvasAnim_GameOver.Instance.Hide();
        GoToMenu();
        CanvasAnim_Game.Instance.Hide();
        CanvasAnim_StartMenu.Instance.Display();
    }

    public bool gameOngoing;
    public void LooseGame()
    {
        GoToMenu();
        PlayerAnimation.Instance.EndSneezing();

        CameraAnimator.Instance.transform.DOMove(PlayerController.Instance.transform.position, 0.3f).OnComplete(() =>
        {
            CanvasAnim_GameOver.Instance.Display();
        });

        MusicManager.Instance.CurrentPlayingSource().Stop();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
