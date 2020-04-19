using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance;

    public GameObject stunStars;

    Animator anim;
    private void Awake()
    {
        Instance = this;

        anim = GetComponent<Animator>();

        stunStars.SetActive(false);
    }

    public void SetStreet()
    {
        anim.SetTrigger("setStreet");
    }

    public void SetCamera()
    {
        anim.SetTrigger("setCamera");
    }

    public void TurnToStreet()
    {
        anim.SetBool("facingCamera", false);
    }

    public void TurnToCamera()
    {
        anim.SetBool("facingCamera", true);
    }
}
