﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfectionWheel : MonoBehaviour
{
    public static InfectionWheel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int wedgeAmount;

    public int xDistance;

    public GameObject wedgeParentPrefab;

    public RectTransform wedgesParent;

    Image[] wedges;

    public void Initialize()
    {
        wedges = new Image[InfectionManager.Instance.maxInfection];
        for(int i = 0; i < InfectionManager.Instance.maxInfection; i++)
        {
            wedges[i] = Instantiate(wedgeParentPrefab, wedgesParent.transform).GetComponentInChildren<Image>();
            wedges[i].transform.parent.GetComponent<RectTransform>().anchoredPosition += new Vector2(xDistance, 0) * i;
            wedges[i].sprite = InfectionManager.Instance.healthyWedge;
        }
    }


    public void RefreshWedgeInfection()
    {
        for(int i = 0; i < InfectionManager.Instance.infection; i++)
        {
            if (wedges[i].sprite != InfectionManager.Instance.infectedWedge)
            {
                InfectWedge(wedges[i]);
            }
        }

    }

    void InfectWedge(Image wedge)
    {
        wedge.sprite = InfectionManager.Instance.infectedWedge;
        wedge.transform.localScale = Vector3.one * 4f;
        wedge.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
    }
}