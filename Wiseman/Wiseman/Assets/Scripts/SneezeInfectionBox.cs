using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezeInfectionBox : MonoBehaviour
{
    public BoxCollider col;

    public void Enable()
    {
        col.enabled = true;
    }

    public void Disable()
    {
        col.enabled = false;
    }

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Disable();
            InfectionManager.Instance.GainInfection(InfectionManager.Instance.sneezeInfectionAmount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Disable();
            InfectionManager.Instance.GainInfection(InfectionManager.Instance.sneezeInfectionAmount);
        }
    }


}
