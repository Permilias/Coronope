using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphics : MonoBehaviour
{
    [Header("Animation")]
    public float advanceBlend;
    public bool advances;

    [Header("Colors")]
    public bool canBeGirl;
    public GameObject girlHair;

    public MeshColorRandomizer[] randomizers;

    Animator anim;


    public void Stun()
    {
        anim.SetBool("stunned", true);
    }

    public void Unstun()
    {
        anim.SetBool("stunned", false);
    }

    public void Initialize()
    {
        anim = GetComponentInChildren<Animator>();

        if(advances)
        {
            anim.SetFloat("WalkingBlend", advanceBlend);
        }


        //randomize everything
        for(int i = 0; i < randomizers.Length; i++)
        {
            randomizers[i].RandomizeColor();
        }

        if(girlHair)
        {
            if (canBeGirl)
            {
                girlHair.SetActive(Random.Range(0, 2) == 0 ? true : false);
            }
            else
            {
                girlHair.SetActive(false);
            }
        }

    }
}

[System.Serializable]
public class MeshColorRandomizer
{
    public SkinnedMeshRenderer[] smrs;
    public MeshRenderer[] mrs;
    public Gradient[] colors;

    public void RandomizeColor()
    {
        Color color = Color.red;
        int i = Random.Range(0, colors.Length);
        color = colors[i].Evaluate(Random.Range(0f, 1f));

        for(int j = 0; j < mrs.Length; j++)
        {
            mrs[j].material.color = color;
        }
        for (int j = 0; j < smrs.Length; j++)
        {
            smrs[j].material.color = color;
        }
    }
}
