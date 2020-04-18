using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public static CameraAnimator Instance;

    

    Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void SetGameplay()
    {
        anim.SetBool("gameplay", true);
    }

    public void SetCinematic()
    {
        anim.SetBool("gameplay", false);
    }
}
