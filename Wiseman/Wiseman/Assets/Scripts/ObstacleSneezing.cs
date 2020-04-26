using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSneezing : MonoBehaviour
{
    Obstacle obstacle;
    public Animator anim;

    private void Awake()
    {
        graphics = GetComponentInChildren<CharacterGraphics>();
    }

    public void Initialize(Obstacle _obstacle)
    {
        obstacle = _obstacle;
        obstacle.ObstacleUpdate += SneezingUpdate;
        

        SetWaiting();

    }

    public float telegraphDuration;
    public float sneezingDuration;
    public float delay;
    public float size;
    int phase;

    CharacterGraphics graphics;

    bool choke;

    float count;
    public void SneezingUpdate()
    {
        if (!obstacle.active) return;

        anim.SetBool("sneezing", true);

        count += Time.deltaTime;
        switch (phase)
        {
            //wait
            case 0:
                if(count >= delay / GameManager.Instance.speedMultiplier)
                {
                    count = 0f;
                    phase = 1;

                    if (graphics != null) graphics.SneezeFace();
                    anim.SetTrigger("startSneezing");
                    anim.speed = 1f/telegraphDuration;
                    StartTelegraph();
                }
                break;
            //charge
            case 1:
                if (count >= telegraphDuration / GameManager.Instance.speedMultiplier)
                {
                    count = 0f;
                    phase = 2;
                    anim.SetTrigger("sneeze");
                    anim.speed = 1f;
                    StopTelegraph();
                    Sneeze();
                }
                break;
            //sneeze
            case 2:
                if (count >= sneezingDuration / GameManager.Instance.speedMultiplier)
                {
                    if (graphics != null) graphics.UnsneezeFace();
                    count = 0f;
                    phase = 0;
                    anim.speed = 1f;
                    SetWaiting();

                }
                break;
        }
    }

    void SetWaiting()
    {

        anim.SetTrigger("setSneezer");
    }

    void StartTelegraph()
    {
        SneezingManager.Instance.Sneeze(this);
    }

    void StopTelegraph()
    {

    }

    void Sneeze()
    {

    }



    public void Deactivate()
    {
        obstacle.ObstacleUpdate -= SneezingUpdate;
    }
}
