using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfectionZone : MonoBehaviour
{
    public SpriteRenderer groundSR;
    public CapsuleCollider col;
    public float size;
    public bool infected;

    public void Initialize(float _size)
    {
        infected = false;
        size = _size;
        transform.localScale = Vector3.one * size;
        //col.radius = size / 2f;
        groundSR.color = InfectionManager.Instance.normalInfectionZoneColor;
    }

    public void Infect()
    {
        infected = true;
        groundSR.color = InfectionManager.Instance.infectedZoneColor;
    }

    public bool playerInside;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInside = false;
        }
    }
}
