using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphics : MonoBehaviour
{
    [Header("Animation")]
    public float advanceBlend;
    public bool advances;

    [Header("Settings")]

    public bool canBeGirl;
    public GameObject girlHair;

    public bool old;
    public bool effort;
    public bool sick;


    public MeshColorRandomizer[] randomizers;
    public Animator anim;
    public SkinnedMeshRenderer faceMR;
    public Sprite[] faceSprites;

    public bool girl;
    public bool blinking;

    public void Stun()
    {
        anim.SetBool("stunned", true);
    }

    public void Unstun()
    {
        anim.SetBool("stunned", false);
    }

    public void RefreshFace()
    {
        faceMR.material.SetTexture("_MouthTexture", faceSprites[0].texture);
        faceMR.material.SetTexture("_EyesTexture", faceSprites[1].texture);
        faceMR.material.SetTexture("_LashesTexture", faceSprites[2].texture);
        faceMR.material.SetTexture("_LidsTexture", faceSprites[3].texture);
    }

    public void SneezeFace()
    {
        faceMR.material.SetTexture("_MouthTexture", CharacterFaceGenerator.Instance.screamingMouth.texture);
        Blink();
    }

    public void UnsneezeFace()
    {
        faceMR.material.SetTexture("_MouthTexture", faceSprites[0].texture);
        Blink();
    }


    public void Blink()
    {
        faceMR.material.SetFloat("_Blinks", 1);
        blinking = true;
    }

    public void StopBlinking()
    {
        faceMR.material.SetFloat("_Blinks", 0);
        blinking = false;
    }

    private void Update()
    {
        if (sick) return;
        if (blinking)
        {
            blinkCount += Time.deltaTime;
            if(blinkCount >= CharacterFaceGenerator.Instance.blinkingDuration)
            {
                blinkCount = 0f;
                StopBlinking();
            }
            return;
        }

        blinkCount += Time.deltaTime;
        if (blinkCount >= blinkDelay)
        {
            blinkDelay = Random.Range(CharacterFaceGenerator.Instance.blinkingMinMaxDelay.x, CharacterFaceGenerator.Instance.blinkingMinMaxDelay.y);
            blinkCount = 0f;
            Blink();
        }



    }
    float blinkCount;
    float blinkDelay;

    public bool player;
    public void Initialize()
    {
        anim = GetComponentInChildren<Animator>();

        if(!player)
        {
            anim.transform.localScale = Vector3.one * (Random.Range(1f, 1.4f));
        }

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
                girl = Random.Range(0, 2) == 0 ? true : false;
                girlHair.SetActive(girl);
            }
            else
            {
                girlHair.SetActive(false);
            }
        }

        CharacterFaceGenerator.Instance.GenerateCharacterFace(this);
    }
}

[System.Serializable]
public class MeshColorRandomizer
{
    public SkinnedMeshRenderer[] smrs;
    public MeshRenderer[] mrs;
    public Gradient[] colors;
    public Color currentColor;

    public void RandomizeColor()
    {
        currentColor = Color.red;
        int i = Random.Range(0, colors.Length);
        currentColor = colors[i].Evaluate(Random.Range(0f, 1f));

        for(int j = 0; j < mrs.Length; j++)
        {
            mrs[j].material.color = currentColor;
        }
        for (int j = 0; j < smrs.Length; j++)
        {
            smrs[j].material.color = currentColor;
        }
    }
}
