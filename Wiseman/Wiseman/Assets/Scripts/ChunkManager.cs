using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkManager : MonoBehaviour
{
    public float obstacleActivationDistance;

    public static ChunkManager Instance;

    public ChunksConfig config;

    ChunkPool[] startingChunkPools;
    ChunkPool[] chunkPools;
    ChunkPool[] collectibleChunkPools;
    ChunkPool[] dropChunkPools;

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

    public Toggle toggle;

    public void ChangeTutorial()
    {
        enableStartingChunks = toggle.isOn;
    }

    public void Initialize()
    {
        startingChunkPools = new ChunkPool[config.startingChunks.Length];
        chunkPools = new ChunkPool[config.normalChunks.Length];
        collectibleChunkPools = new ChunkPool[config.collectibleChunks.Length];
        dropChunkPools = new ChunkPool[config.dropChunks.Length];


        for (int i = 0; i < config.startingChunks.Length; i++)
        {
            startingChunkPools[i] = new ChunkPool(config.startingChunks[i]);
        }

        for (int i = 0; i < config.normalChunks.Length; i++)
        {
            chunkPools[i] = new ChunkPool(config.normalChunks[i]);
        }

        for (int i = 0; i < config.collectibleChunks.Length; i++)
        {
            collectibleChunkPools[i] = new ChunkPool(config.collectibleChunks[i]);
        }

        for (int i = 0; i < config.dropChunks.Length; i++)
        {
            dropChunkPools[i] = new ChunkPool(config.dropChunks[i]);
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
                if(i == 0)
                {
                    chunkParents[i].position = chunkParents[2].position + new Vector3(0, 0, config.chunkSize);
                }
                else
                {
                    chunkParents[i].position = chunkParents[i-1].position + new Vector3(0, 0, config.chunkSize);
                }


                GenerateNewChunk(i);

                chunkParents[i].localScale = new Vector3(Random.Range(0, 2) == 0 ? -1 : 1, 1, 1);

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

            switch(currentChunks[parentIndex].type)
            {
                case ChunkType.normal:
                    chunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
                case ChunkType.starting:
                    startingChunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
                case ChunkType.collectible:
                    collectibleChunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
                case ChunkType.drop:
                    dropChunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
            }
        }


        currentChunks[parentIndex] = GetChunk();
        currentChunks[parentIndex].Initialize();
        currentChunks[parentIndex].gameObject.SetActive(true);
        currentChunks[parentIndex].transform.parent = chunkParents[parentIndex];
        currentChunks[parentIndex].transform.localPosition = Vector3.zero;
    }

    int progression;
    int chunkIndex;
    int collectibleCount;
    int dropCount;
    public bool enableStartingChunks;
    bool startingChunks;
    public bool inTutorial;
    public Chunk GetChunk()
    {
        Chunk returned;

        startingChunks = false;

        if (enableStartingChunks) startingChunks = true;
        if (progression == 0) startingChunks = true;

        if (progression < startingChunkPools.Length && startingChunks)
        {
            inTutorial = true;
            returned = startingChunkPools[progression].Dequeue();
            returned.type = ChunkType.starting;
            returned.poolIndex = progression;
        }
        else
        {
            inTutorial = false;
            if (dropCount >= config.dropChunkDelayCount)
            {
                int index = Random.Range(0, dropChunkPools.Length);
                returned = dropChunkPools[index].Dequeue();
                dropCount = 0;

                returned.poolIndex = index;
                returned.type = ChunkType.drop;
            }
            else if(collectibleCount >= config.collectibleChunkDelayCount)
            {
                dropCount++;
                int rand = Random.Range(0, config.collectibleChunkOdds);
                if(rand == 0)
                {
                    int index = Random.Range(0, collectibleChunkPools.Length);
                    returned = collectibleChunkPools[index].Dequeue();
                    collectibleCount = 0;

                    returned.poolIndex = index;
                    returned.type = ChunkType.collectible;

                }
                else
                {
                    int index = Random.Range(0, chunkPools.Length);
                    returned = chunkPools[index].Dequeue();


                    returned.poolIndex = index;
                    returned.type = ChunkType.normal;

                }

            }
            else
            {
                dropCount++;
                int index = Random.Range(0, chunkPools.Length);

                collectibleCount++;
                returned = chunkPools[index].Dequeue();

                returned.poolIndex = index;
                returned.type = ChunkType.normal;

            }
        }


        progression++;

        return returned;
    }
}
