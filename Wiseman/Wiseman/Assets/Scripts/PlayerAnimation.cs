using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance;

    public GameObject stunStars;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftBagHand;
    public GameObject rightBagHand;

    public GameObject[] bags;

    public int[] bagsThresholds;

    
    public void Orient()
    {

    }

    public void RefreshBags()
    {
        int currentThreshold = -1;
        for(int i = 0;i  < bagsThresholds.Length; i++)
        {
            if(bagsThresholds[i] <= CollectibleManager.Instance.masksPossessed)
            {
                currentThreshold = i;
            }
        }

        if (currentThreshold >= bags.Length) currentThreshold = bags.Length - 1;

        if(currentThreshold >= 0)
        {

            leftHand.SetActive(true);
            leftBagHand.SetActive(true);
            if (currentThreshold >= 1)
            {

                rightHand.SetActive(true);
                rightBagHand.SetActive(true);
            }
            else
            {

                rightHand.SetActive(false);
                rightBagHand.SetActive(false);
            }
        }
        else
        {

            leftHand.SetActive(false);
            leftBagHand.SetActive(false);
        }

        leftHand.SetActive(true);
        leftBagHand.SetActive(false);
        rightHand.SetActive(true);
        rightBagHand.SetActive(false);
        

        for (int i = 0; i < bags.Length; i++)
        {


            if (i <= currentThreshold)
            {
                bags[i].SetActive(true);
            }
            else
            {
                bags[i].SetActive(false);

            }


        }
    }

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
        RefreshBags();
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
    
    public void EndSneezing()
    {
        PlayerController.Instance.Stop();
        graphics.anim.SetBool("sneezing", true);
        graphics.anim.SetTrigger("startSneezing");
        graphics.anim.SetTrigger("sneeze");

    }
}
