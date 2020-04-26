using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    public Transform pointTransform;
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CollectibleManager.Instance.DropAllCollectibles(pointTransform);
            anim.SetTrigger("take");
        }
    }

    
}
