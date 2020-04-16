using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfectionZone : MonoBehaviour
{
    public Projector projector;
    public CapsuleCollider col;
    public float size;
    public bool infected;

    public void Initialize(float _size)
    {
        infected = false;
        size = _size;
        projector.fieldOfView = size * 10;
        col.radius = size / 2f;
        projector.material = InfectionManager.Instance.normalProjectorMaterial;
    }

    public void Infect()
    {
        infected = true;
        projector.material = InfectionManager.Instance.infectedProjectorMaterial;
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
