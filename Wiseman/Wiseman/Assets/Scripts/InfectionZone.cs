using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfectionZone : MonoBehaviour
{
    public MeshRenderer groundTextureMR;
    public CapsuleCollider col;
    public float size;
    public bool infected;

    public void Initialize(float _size)
    {
        infected = false;
        size = _size;
        transform.localScale = Vector3.one * size;
        //col.radius = size / 2f;
        groundTextureMR.material = InfectionManager.Instance.normalProjectorMaterial;
    }

    public void Infect()
    {
        infected = true;
        groundTextureMR.material = InfectionManager.Instance.infectedProjectorMaterial;
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
