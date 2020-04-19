using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    public static FXPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public FXConfig config;

    public FXPool[] fxPools;

    public void Initialize()
    {
        fxPools = new FXPool[config.fxs.Length];
        for(int i = 0; i < config.fxs.Length; i++)
        {
            GameObject newPoolGO = new GameObject("FXPool_" + config.fxs[i].fxName);
            newPoolGO.transform.parent = transform;
            FXPool newPool = newPoolGO.AddComponent<FXPool>();
            newPool.Initialize(config.fxs[i]);
            fxPools[i] = newPool;
        }
    }

    public void PlayFX(string fxName, Transform _transform)
    {
        bool found = false;
        for(int i = 0; i < fxPools.Length; i++)
        {
            if(fxPools[i].data.fxName == fxName)
            {
                found = true;
                PlayFXFromPool(fxPools[i], _transform);
                break;
            }
        }

        if(!found)
        {
            Debug.LogError("ERROR : No FX named " + fxName + " !");
        }
    }

    public void PlayTextMessage(Transform _transform, Color color, string text, float height)
    {
        bool found = false;
        for (int i = 0; i < fxPools.Length; i++)
        {
            if (fxPools[i].data.fxName == "TextMessage")
            {
                found = true;
                PlayFXFromPool(fxPools[i], _transform, true, color, text, height);
                break;
            }
        }

        if (!found)
        {
            Debug.LogError("ERROR : No Text Message !");
        }
    }

    void PlayFXFromPool(FXPool pool, Transform _transform)
    {
        FX playedFX = pool.Depool();
        playedFX.transform.position = _transform.position;
        playedFX.Play();
    }

    void PlayFXFromPool(FXPool pool, Transform _transform, bool textMessage, Color color, string text, float height)
    {
        FX playedFX = pool.Depool();
        playedFX.transform.parent = _transform;
        playedFX.transform.localPosition = Vector3.zero;

        if(textMessage)
        {
            TextMessage message = (TextMessage)playedFX;
            message.SetTextAndPlay(text, color, height);
        }
        else
        {
            playedFX.Play();
        }

    }
}
