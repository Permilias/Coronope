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
        wedges[wedges.Count - 1].sprite = InfectionManager.Instance.saneWedge;
        wedges[wedges.Count - 1].color = InfectionManager.Instance.saneColor;
        wedges[wedges.Count - 1].transform.localScale = Vector3.one * 4f;
        wedges[wedges.Count - 1].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);

        InfectionWheel.Instance.RefreshWedgeInfection();
    }

    
    public void RefreshWedgeInfection()
    {
        for(int i = 0; i < wedges.Count; i++)
        {
            wedges[i].color = InfectionManager.Instance.saneColor;
            wedges[i].sprite = InfectionManager.Instance.saneWedge;
            wedges[i].transform.localScale = Vector3.one;
        }


        for (int i = 0; i < InfectionManager.Instance.infection; i++)
        {
            if(i < wedges.Count)
            {
                InfectWedge(wedges[wedges.Count - i - 1]);

                if (i >= currentInfection)
                {
                    currentInfection++;
                }
            }
        }


    }

    int currentInfection;

    void InfectWedge(Image wedge)
    {
        bool flates = currentInfection < InfectionManager.Instance.infection ? true : false;

        wedge.color = InfectionManager.Instance.infectedColor;
        wedge.sprite = InfectionManager.Instance.infectedWedge;


        if(flates)
        {
            wedge.transform.localScale = Vector3.one;
            wedge.DOFade(0f, 0.8f);
            wedge.transform.DOScale(Vector3.one * 2f, 0.4f).OnComplete(() =>
            {
 
            });
        }
        else
        {
            wedge.color = new Color(1, 1, 1, 0);
        }

    }
}
