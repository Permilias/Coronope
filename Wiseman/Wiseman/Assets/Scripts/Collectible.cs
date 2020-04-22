using System.Collections;
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

    public bool mask;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, CollectibleManager.Instance.spinningSpeed * Time.deltaTime, 0);
    }

    public void Initialize(CollectibleData _data)
    {
        collected = false;
        CollectibleManager.Instance.currentCollectibles.Add(this);
        col.enabled = true;
        transform.localScale = Vector3.one;
        Refresh(_data);


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
            graphics.transform.localScale = Vector3.one;
            graphics.gameObject.SetActive(true);
            graphics.transform.parent = graphicsParent;
            graphics.transform.localPosition = Vector3.zero;
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



        graphics.transform.DOScale(Vector3.one * 1.3f, CollectibleManager.Instance.shrinkingSpeed);
        graphics.transform.DOLocalMove(graphics.transform.localPosition + new Vector3(0, 3, 0), CollectibleManager.Instance.shrinkingSpeed).OnComplete(() =>
        {
            graphics.transform.DOScale(Vector3.zero, CollectibleManager.Instance.shrinkingSpeed);
            graphics.transform.DOLocalMove(graphics.transform.localPosition + new Vector3(0, 0, 0), CollectibleManager.Instance.shrinkingSpeed).OnComplete(() =>
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
        if(collider.gameObject.tag == "Player")
        {
            Collect();
        }
    }
}


