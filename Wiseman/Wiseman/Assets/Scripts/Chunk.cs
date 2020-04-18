using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int poolIndex;

    public ChunkType type;

    Obstacle[] obstacles;
    Collectible[] collectibles;
    ChunkSideDecorumGenerator decorum;

    private void Awake()
    {
        obstacles = GetComponentsInChildren<Obstacle>();
        collectibles = GetComponentsInChildren<Collectible>();
        decorum = GetComponentInChildren<ChunkSideDecorumGenerator>();
    }

    public void Initialize()
    {

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].Initialize();
        }

        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].Initialize(CollectibleManager.Instance.GetSuitableData());
        }

        decorum.Initialize();


    }


    public void Deactivate()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].Deactivate();
        }


        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].Deactivate();
        }

        GetComponentInChildren<ChunkSideDecorumGenerator>().Deactivate();
    }

}

public enum ChunkType
{
    starting,
    normal,
    collectible,
    drop
}