using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    public Transform pointTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CollectibleManager.Instance.DropAllCollectibles(pointTransform);

        }
    }

    
}
