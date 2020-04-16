using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezingManager : MonoBehaviour
{
    public static SneezingManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Material telegraphMaterial;
    public Material dangerMaterial;

    public float closingSpeed;

    public GameObject sneezeEffectPrefab;

    Queue<SneezeEffect> sneezeEffectsPool;


    public void Initialize()
    {
        sneezeEffectsPool = new Queue<SneezeEffect>();
        FillSneezePool();
    }

    void FillSneezePool()
    {
        for(int i =0; i < 10; i++)
        {
            SneezeEffect newSneeze = Instantiate(sneezeEffectPrefab, transform).GetComponent<SneezeEffect>();
            sneezeEffectsPool.Enqueue(newSneeze);
            newSneeze.gameObject.SetActive(false);
        }
    }

    public void Sneeze(ObstacleSneezing sneezer)
    {
        if(sneezeEffectsPool.Count < 1)
        {
            FillSneezePool();
        }
        SneezeEffect newSneeze = sneezeEffectsPool.Dequeue();
        newSneeze.gameObject.SetActive(true);
        newSneeze.Sneeze(sneezer);
    }

    public void Repool(SneezeEffect effect)
    {
        effect.transform.parent = transform;
        sneezeEffectsPool.Enqueue(effect);
        effect.gameObject.SetActive(false);
    }
}
