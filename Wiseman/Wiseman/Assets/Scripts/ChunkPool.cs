using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPool
{
    public GameObject prefab;
    public Queue<Chunk> pool;

    public ChunkPool(GameObject _prefab)
    {
        prefab = _prefab;
        Initialize();
    }

    public void Initialize()
    {
        pool = new Queue<Chunk>();
        FillPool();
    }

    public void FillPool()
    {
        Chunk newChunk = GameObject.Instantiate(prefab, ChunkManager.Instance.inactiveChunksParent).GetComponent<Chunk>();
        
        Requeue(newChunk);
    }
    
    public Chunk Dequeue()
    {
        if(pool.Count < 1)
        {
            FillPool();
        }

        return pool.Dequeue();
    }

    public void Requeue(Chunk chunk)
    {
        pool.Enqueue(chunk);
        chunk.gameObject.SetActive(false);
    }
}
