using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSneezing : MonoBehaviour
{
    Obstacle obstacle;

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
                    StartTelegraph();
                }
                break;
            //charge
            case 1:
                if (count >= telegraphDuration / GameManager.Instance.speedMultiplier)
                {
                    count = 0f;
                    phase = 2;
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
