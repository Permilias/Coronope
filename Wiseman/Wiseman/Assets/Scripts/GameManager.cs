using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float speedMultiplier;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        CollectibleManager.Instance.Initialize();

        InfectionManager.Instance.Initialize();
        SneezingManager.Instance.Initialize();

        DecorumManager.Instance.Initialize();

        ChunkManager.Instance.Initialize();

    }
}
