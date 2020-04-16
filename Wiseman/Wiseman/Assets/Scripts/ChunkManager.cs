using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public float obstacleActivationDistance;

    public static ChunkManager Instance;

    public ChunksConfig config;

    public GameObject[] chunkPrefabs;
    ChunkPool[] chunkPools;

    public Transform inactiveChunksParent;

    public int nextChunkIndex;

    public float currentSpeed;

    private void Awake()
    {
        Instance = this;

        currentSpeed = config.baseSpeed;
    }

    Transform[] chunkParents;
    Chunk[] currentChunks;

    public void Initialize()
    {
        chunkPools = new ChunkPool[chunkPrefabs.Length];
        for (int i = 0; i < chunkPrefabs.Length; i++)
        {
            chunkPools[i] = new ChunkPool(chunkPrefabs[i]);
        }

        chunkParents = new Transform[3];
        currentChunks = new Chunk[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject newParent = new GameObject("ChunkParent_" + i.ToString());
            newParent.transform.parent = transform;
            chunkParents[i] = newParent.transform;
            chunkParents[i].position = new Vector3(0, 0, -config.chunkSize + (config.chunkSize * i));

            GenerateNewChunk(i);
        }
    }

    private void Update()
    {
        UpdateChunksMovement();
    }

    void UpdateChunksMovement()
    {
        for(int i =0; i < chunkParents.Length; i++)
        {
            chunkParents[i].position += new Vector3(0, 0, -currentSpeed * Time.deltaTime * GameManager.Instance.speedMultiplier);
            if(chunkParents[i].position.z <= -config.chunkSize*2)
            {
                float leftOver = -config.chunkSize * 2 - chunkParents[i].position.z;
                chunkParents[i].position = new Vector3(0, 0, config.chunkSize + leftOver);
                GenerateNewChunk(i);
            }
        }
    }

    void GenerateNewChunk(int parentIndex)
    {
        if(currentChunks[parentIndex] != null)
        {
            currentChunks[parentIndex].Deactivate();
            currentChunks[parentIndex].gameObject.SetActive(false);
            currentChunks[parentIndex].transform.parent = inactiveChunksParent;
            chunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
        }


        UpdateChunkIndex();

        currentChunks[parentIndex] = GetChunk(chunkIndex);
        currentChunks[parentIndex].Initialize(chunkIndex);
        currentChunks[parentIndex].gameObject.SetActive(true);
        currentChunks[parentIndex].transform.parent = chunkParents[parentIndex];
        currentChunks[parentIndex].transform.localPosition = Vector3.zero;
    }

    int progression;
    int chunkIndex;
    public void UpdateChunkIndex()
    {
        if (progression == 0) chunkIndex = 0;

        else
        {
            chunkIndex = Random.Range(1, chunkPools.Length);
        }

        progression++;
    }

    public Chunk GetChunk(int index)
    {
        return chunkPools[index].Dequeue();
    }
}
