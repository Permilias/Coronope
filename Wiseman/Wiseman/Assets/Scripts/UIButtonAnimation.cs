using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIButtonAnimation : MonoBehaviour
{
    public float bonusScale;
    public float scalingSpeed;
    public float closingSpeed;
    public Ease scalingEase;
    public Ease closingEase;
    public void Hover()
    {
        transform.DOScale(Vector3.one * bonusScale, scalingSpeed).SetEase(scalingEase);
    }

    public void StopHovering()
    {
        transform.DOScale(Vector3.one, scalingSpeed).SetEase(scalingEase);
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, closingSpeed).SetEase(scalingEase);
    }

}
