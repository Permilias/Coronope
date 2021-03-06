﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    public bool collected;
    public CollectibleData data;
    [HideInInspector]
    public Collider col;
    public Transform graphicsParent;
    public GameObject graphics;

    public float collectHeight;

    public Transform shiningTransform;

    public int forceDataIndex;
    public bool forcesConfig;

    public bool mask;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        graphicsParent.transform.localEulerAngles += new Vector3(0, CollectibleManager.Instance.spinningSpeed * Time.deltaTime, 0);
    }

    public void Initialize(CollectibleData _data)
    {
        if (mask)
        {
            shiningTransform.gameObject.SetActive(true);
        }

        collected = false;
        CollectibleManager.Instance.currentCollectibles.Add(this);
        col.enabled = true;
        transform.localScale = Vector3.one;

        if(forcesConfig)
        {
            Refresh(CollectibleManager.Instance.dataToCollect[forceDataIndex]);

        }
        else
        {
            Refresh(_data);

        }


    }



    public void Refresh(CollectibleData _data)
    {
        if (collected) return;
        data = _data;


        if (mask)
        {
            graphics.transform.localScale = Vector3.one;
            graphics.gameObject.SetActive(true);
            return;
        }

        RepoolGraphics();

        graphics = data.Depool();

        if (graphics != null)
        {
            graphicsParent.gameObject.SetActive(true);

            graphics.gameObject.SetActive(true);
            graphics.transform.parent = graphicsParent;
            graphics.transform.localPosition = Vector3.zero;
            graphics.transform.localScale = Vector3.one;
        }
    }

    public void RepoolGraphics()
    {
        if (graphics != null)
        {
            data.Repool(graphics);
            graphics = null;
        }
    }

    public void Deactivate()
    {
        CollectibleManager.Instance.currentCollectibles.Remove(this);
        if (graphics!= null && ! mask)
        {
            data.Repool(graphics);
        }
 
    }

    public void Collect()
    {
        collected = true;
        CollectibleManager.Instance.Collect(this);
        print("collected !");

        if(mask)
        {
            shiningTransform.gameObject.SetActive(false);
        }


        graphics.transform.DOScale(Vector3.one * 1.5f, CollectibleManager.Instance.raisingSpeed);
        graphics.transform.DOLocalMove(graphics.transform.localPosition + new Vector3(0, collectHeight, 0), CollectibleManager.Instance.raisingSpeed).OnComplete(() =>
        {
            graphics.transform.DOScale(Vector3.zero, CollectibleManager.Instance.shrinkingSpeed);
            graphics.transform.DOLocalMove(graphics.transform.localPosition, CollectibleManager.Instance.shrinkingSpeed).OnComplete(() =>
            {
                if (!mask)
                {
                    RepoolGraphics();
                }
            });
        });
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && !collected)
        {
            Collect();
        }
    }
}


