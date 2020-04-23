using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    public float spinningSpeed;

    public float shrinkingSpeed;

    public int masksPossessed;

    public int gainMultiplierPerCollectible;

    public CollectibleConfig maskConfig;
    public CollectibleData maskData;

    public TextMeshProUGUI masksTextMesh;

    private void Awake()
    {
        Instance = this;
    }

    public CollectiblesConfig config;

    public List<CollectibleData> dataToCollect;

    public List<Collectible> currentCollectibles;

    public void Initialize()
    {
        maskData = new CollectibleData(maskConfig);

        currentCollectibles = new List<Collectible>();

        dataToCollect = new List<CollectibleData>();

        for(int i = 0; i < config.configs.Length; i++)
        {
            dataToCollect.Add(new CollectibleData(config.configs[i]));
        }

        masksTextMesh.text = "x" + masksPossessed.ToString();
    }


    public void DropAllCollectibles(Transform dropTransform)
    {
        ScoreManager.Instance.GainLivesSaved(gainMultiplierPerCollectible * masksPossessed);
        FXPlayer.Instance.PlayTextMessage(dropTransform, Color.white, "+ " + (gainMultiplierPerCollectible * masksPossessed).ToString() + " lives !", 4f);

        masksPossessed = 0;

        PlayerController.Instance.RefreshMovementValues();
        masksTextMesh.text = "x" + masksPossessed.ToString();


    }

    public void Collect(Collectible collectible)
    {
        
        collectible.col.enabled = false;

        if(collectible.mask)
        {
            masksPossessed++;
        }


        for(int i = 0; i < currentCollectibles.Count; i++)
        {
            if(!currentCollectibles[i].collected)
            {
                currentCollectibles[i].Refresh(GetSuitableData());
            }

        }

        masksTextMesh.text = "x" + masksPossessed.ToString();
        PlayerController.Instance.RefreshMovementValues();
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