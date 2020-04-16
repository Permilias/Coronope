using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfection : MonoBehaviour
{
    public float infectionDiameter;

    public InfectionZone zone;

    public void Initialize()
    {
        zone = InfectionManager.Instance.GenerateInfectionZone();
        zone.transform.parent = transform;
        zone.transform.position = transform.position;
        InfectionManager.Instance.currentObstacleInfections.Add(this);
        zone.gameObject.SetActive(true);
        zone.Initialize(infectionDiameter);
    }



    public void Deactivate()
    {
        
        InfectionManager.Instance.RepoolInfectionZone(zone);
        zone = null;
        InfectionManager.Instance.currentObstacleInfections.Remove(this);
    }
}
