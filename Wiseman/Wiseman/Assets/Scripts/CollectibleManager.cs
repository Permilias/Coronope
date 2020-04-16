using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    public float spinningSpeed;

    public float shrinkingSpeed;

    private void Awake()
    {
        Instance = this;
    }

    public CollectiblesConfig config;

    public List<CollectibleData> dataToCollect;

    public List<Collectible> currentCollectibles;

    public void Initialize()
    {
        currentCollectibles = new List<Collectible>();

        dataToCollect = new List<CollectibleData>();

        for(int i = 0; i < 2; i++)
        {
            dataToCollect.Add(new CollectibleData(config.configs[i]));
        }
    }

    public void Collect(Collectible collectible)
    {
        dataToCollect.Remove(collectible.data);
        collectible.transform.DOScale(Vector3.zero, shrinkingSpeed).SetEase(Ease.InBack, 1.5f);
        collectible.col.enabled = false;

        for(int i = 0; i < currentCollectibles.Count; i++)
        {
            if(!currentCollectibles[i].collected)
            {
                currentCollectibles[i].Refresh(GetSuitableData());
            }

        }
    }

    public CollectibleData placeholderData;
    public CollectibleData GetSuitableData()
    {
        if(dataToCollect.Count < 1)
        {
            print("game won, cannot generate more collectibles");
            return placeholderData;
        }

        return dataToCollect[Random.Range(0, dataToCollect.Count)];
    }
}

[System.Serializable]
public class CollectibleData
{
    public CollectibleConfig config;

    public CollectibleData(CollectibleConfig _config)
    {
        config = _config;
        pool = new Queue<GameObject>();
        FillPool();
    }

    Queue<GameObject> pool;
    void FillPool()
    {
        GameObject newGraphics = GameObject.Instantiate(config.graphicsPrefab, CollectibleManager.Instance.transform);
        Repool(newGraphics);
    }

    public GameObject Depool()
    {
        if(pool.Count < 1)
        {
            FillPool();
        }
        return pool.Dequeue();
    }

    public void Repool(GameObject graphics)
    {
        if(graphics != null)
        pool.Enqueue(graphics);
        graphics.SetActive(false);
    }
}

[System.Serializable]
public class CollectibleConfig
{
    public string collectibleName;
    public GameObject graphicsPrefab;
}