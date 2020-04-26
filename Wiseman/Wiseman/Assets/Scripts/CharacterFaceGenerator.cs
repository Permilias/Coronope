using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFaceGenerator : MonoBehaviour
{
    public static CharacterFaceGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Material baseMaterial;

    public Sprite emptySprite;
    public Sprite normalEyes;


    public Sprite[] lashes;
    public Sprite sickLids;
    public Sprite happyLids;
    public Sprite lazyLids;
    public Sprite angryLids;
    public Sprite oldLids;

    public Sprite[] girlMouths;
    public Sprite[] boyMouths;
    public Sprite[] oldMouths;
    public Sprite effortMouth;
    public Sprite screamingMouth;


    public Sprite playerMouth;

    public Vector2 blinkingMinMaxDelay;
    public float blinkingDuration;

    public void GenerateCharacterFace(CharacterGraphics graphics)
    {
        Material faceMaterial = baseMaterial;
        graphics.faceMR.material = faceMaterial;

        graphics.faceMR.material.SetColor("_SkinColor", graphics.randomizers[1].currentColor);

        graphics.faceSprites = new Sprite[4];


        //eyes & mouth
        graphics.faceSprites[1] = normalEyes;
        graphics.faceSprites[2] = emptySprite;
        graphics.faceSprites[3] = emptySprite;

        if (graphics.old)
        {
            graphics.faceSprites[0] = oldMouths[Random.Range(0, oldMouths.Length)];
        }
        else if(graphics.effort)
        {
            graphics.faceSprites[0] = effortMouth;
        }
        else
        {
            //lashes & mouth
            if(graphics.girl)
            {
                graphics.faceSprites[0] = girlMouths[Random.Range(0, girlMouths.Length)];
                Sprite chosenSprite = lashes[Random.Range(0, lashes.Length)];
                graphics.faceSprites[2] = chosenSprite;
            }
            else
            {
                graphics.faceSprites[0] = boyMouths[Random.Range(0, boyMouths.Length)];
                graphics.faceSprites[2] = emptySprite;
            }

            if(graphics.player)
            {
                graphics.faceSprites[0] = playerMouth;
            }

            graphics.faceSprites[3] = emptySprite;
            //lids
            if (graphics.sick)
            {
                graphics.faceSprites[3] = sickLids;
            }
            else if(graphics.old)
            {
                graphics.faceSprites[3] = oldLids;
            }
            else
            {
                if(!graphics.player)
                {
                    int i = Random.Range(0,5);
                    if (i == 3)
                    {
                        graphics.faceSprites[3] = happyLids;
                    }
                    else if (i == 4)
                    {
                        graphics.faceSprites[3] = lazyLids;
                    }

                }

            }
        }


        graphics.RefreshFace();


    }
}
