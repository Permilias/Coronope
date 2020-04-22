using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool collected;
    public CollectibleData data;
    [HideInInspector]
    public Collider col;
    public Transform graphicsParent;
    GameObject graphics;

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
        if (graphics!= null)
        {
            data.Repool(graphics);
        }
 
    }

    public void Collect()
    {
        collected = true;
        CollectibleManager.Instance.Collect(this);
        print("collected !");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Collect();
        }
    }
}


