using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject zonePrefab;

    public List<ObstacleInfection> currentObstacleInfections;

    Queue<InfectionZone> zonePool;

    public bool playerInZone;

    public Sprite saneWedge;
    public Sprite infectedWedge;

    public Color normalInfectionZoneColor;
    public Color infectedZoneColor;

    public Material normalProjectorMaterial;
    public Material infectedProjectorMaterial;

    [Header("Infection")]
    public int infection;
    public int maxInfection;
    public int sneezeInfectionAmount;
    public Color infectionMessageColor;
    public float infectionMessageHeight;


    public Color saneColor;
    public Color infectedColor;

    public void GainInfection(int amount)
    {
        if(amount >= 0)
        {
            ScoreManager.Instance.LooseLivesSaved(ScoreManager.Instance.currentGain);
        }

        infection += amount;
        if(infection >= maxInfection && GameManager.Instance.gameOngoing)
        {
            infection = maxInfection;
            GameManager.Instance.LooseGame();
            InfectionWheel.Instance.RefreshWedgeInfection();
            return;
        }
        if (infection < 0) infection = 0;

        GameManager.Instance.ResetSpeed();
        InfectionWheel.Instance.RefreshWedgeInfection();

       
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.U))
        {
            GainInfection(1);
        }

        bool o = false;
        for(int i = 0; i < currentObstacleInfections.Count; i++)
        {
            if(currentObstacleInfections[i].zone.playerInside)
            {
                if(!currentObstacleInfections[i].zone.infected)
                {
                    currentObstacleInfections[i].zone.Infect();
                    GainInfection(1);
                    o = true;
                }

            }
        }

        playerInZone = o;
    }

    public void Initialize()
    {
        InfectionWheel.Instance.Initialize();

        zonePool = new Queue<InfectionZone>();
        FillZonePool();

    }

    void FillZonePool()
    {
        for(int i =0; i < 10; i++)
        {
            InfectionZone newZone = Instantiate(zonePrefab, transform).GetComponent<InfectionZone>();
            zonePool.Enqueue(newZone);
            newZone.gameObject.SetActive(false);
        }
    }

    public InfectionZone GenerateInfectionZone()
    {
        if(zonePool.Count < 1)
        {
            FillZonePool();
        }

        return zonePool.Dequeue();
    }

    public void RepoolInfectionZone(InfectionZone zone)
    {
        if (zone == null) return;
        zone.transform.parent = transform;
        zone.gameObject.SetActive(false);
        zonePool.Enqueue(zone);
    }
}
