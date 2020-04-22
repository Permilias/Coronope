using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSneezing : MonoBehaviour
{
    Obstacle obstacle;
    public Animator anim;

    public void Initialize(Obstacle _obstacle)
    {
        obstacle = _obstacle;
        obstacle.ObstacleUpdate += SneezingUpdate;


        SetWaiting();

        anim.SetTrigger("setSneezer");
    }

    public float telegraphDuration;
    public float sneezingDuration;
    public float delay;
    public float size;
    int phase;

    bool choke;

    float count;
    public void SneezingUpdate()
    {
        if (!obstacle.active) return;
        

        count += Time.deltaTime;
        switch (phase)
        {
            //wait
            case 0:
                if(count >= delay / GameManager.Instance.speedMultiplier)
                {
                    count = 0f;
                    phase = 1;
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
        print("sneezing !");
    }



    public void Deactivate()
    {
        obstacle.ObstacleUpdate -= SneezingUpdate;
    }
}
