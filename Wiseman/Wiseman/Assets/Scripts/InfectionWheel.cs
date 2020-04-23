using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    List<Image> wedges;

    public void Initialize()
    {
        wedges = new List<Image>();
        for(int i = 0; i < InfectionManager.Instance.maxInfection; i++)
        {
            GainWedge();
        }
    }

    public void GainWedge()
    {
        wedges.Add(Instantiate(wedgeParentPrefab, wedgesParent.transform).GetComponentInChildren<Image>());
        wedges[wedges.Count-1].transform.parent.GetComponent<RectTransform>().anchoredPosition += new Vector2(xDistance, 0) * (wedges.Count - 1);
        wedges[wedges.Count - 1].sprite = InfectionManager.Instance.healthyWedge;
        wedges[wedges.Count - 1].transform.localScale = Vector3.one * 4f;
        wedges[wedges.Count - 1].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
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
