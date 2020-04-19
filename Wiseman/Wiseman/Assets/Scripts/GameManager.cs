using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DifficultyConfig difficultyConfig;

    float growingSpeedMult;
    public float speedMultiplier;

    private void Awake()
    {
        Instance = this;

        growingSpeedMult = speedMultiplier;
        speedMultiplier = 0f;
    }

    private void Start()
    {
        FXPlayer.Instance.Initialize();
        CollectibleManager.Instance.Initialize();

        InfectionManager.Instance.Initialize();
        SneezingManager.Instance.Initialize();

        DecorumManager.Instance.Initialize();

        ChunkManager.Instance.Initialize();

        PlayerController.Instance.RefreshMovementValues();


        StartMenu();
    }

    public void StartGame()
    {
        speedMultiplier = growingSpeedMult;
        CameraAnimator.Instance.SetGameplay();
        PlayerAnimation.Instance.TurnToStreet();
        CanvasAnim_Game.Instance.Display();
        CanvasAnim_StartMenu.Instance.Hide();
    }

    public void GoToMenu()
    {
        speedMultiplier = 0;
        CameraAnimator.Instance.SetCinematic();
        PlayerAnimation.Instance.TurnToCamera();
        CanvasAnim_Game.Instance.Hide();
    }

    public void StartMenu()
    {
        GoToMenu();
        CanvasAnim_StartMenu.Instance.Display();
    }
}
