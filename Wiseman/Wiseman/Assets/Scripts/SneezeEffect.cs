using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneezeEffect : MonoBehaviour
{
    public Transform telegraphParent;
    public MeshRenderer mr;

    public SneezeInfectionBox infectionBox;


    public float width = 0f;

    public void Initialize()
    {
        width = 0;
        telegraphParent.localScale = Vector3.zero;
        infectionBox.Disable();
    }

    public void Sneeze(ObstacleSneezing sneezer)
    {
        infectionBox.col.size = new Vector3(sneezer.size, 3, 60);
        infectionBox.Disable();
        transform.position = new Vector3(sneezer.transform.position.x, 0, sneezer.transform.position.z);
        transform.parent = sneezer.transform;
        transform.rotation = sneezer.transform.rotation;

        mr.material = SneezingManager.Instance.telegraphMaterial;

        Sequence sneezingSequence = DOTween.Sequence();

        sneezingSequence.Append(DOTween.To(() => width, x => width = x, sneezer.size, sneezer.telegraphDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            mr.material = SneezingManager.Instance.dangerMaterial;
            infectionBox.Enable();
        }));

        sneezingSequence.AppendInterval(sneezer.sneezingDuration).OnComplete(() =>
        {
            infectionBox.Disable();
        });

        sneezingSequence.Append(DOTween.To(() => width, x => width = x, 0f, SneezingManager.Instance.closingSpeed).SetEase(Ease.InBack).OnComplete(() =>
        {
            Repool();
        }));


        sneezingSequence.Play();
    }



    private void Update()
    {
        telegraphParent.transform.localScale = Vector3.one * width;
    }

    void Repool()
    {
        
        SneezingManager.Instance.Repool(this);
    }
}
