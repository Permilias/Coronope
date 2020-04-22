using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance;

    public GameObject stunStars;


    public CharacterGraphics graphics;

    Animator anim;
    private void Awake()
    {
        Instance = this;

        anim = GetComponent<Animator>();

        stunStars.SetActive(false);

    }

    public void Initialize()
    {
        graphics.Initialize();

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
        graphics.anim.SetBool("mainIdle", false);
        anim.SetBool("facingCamera", false);
    }

    public void TurnToCamera()
    {
        graphics.anim.SetBool("mainIdle", true);
        anim.SetBool("facingCamera", true);
    }
}
