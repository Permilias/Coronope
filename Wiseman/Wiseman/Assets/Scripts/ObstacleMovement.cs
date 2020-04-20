using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ObstacleMovement : MonoBehaviour
{
    public float baseSpeed;
    float currentSpeed;
    Obstacle obstacle;
    public bool telegraphs;
    public GameObject telegraph;
    public float telegraphSpeed;
    bool telegraphDeployed;
    private void Awake()
    {
        if (telegraphs)
        {
            telegraph.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    public void Initialize(Obstacle _obstacle)
    {
        telegraphDeployed = false;
        obstacle = _obstacle;
        currentSpeed = baseSpeed * GameManager.Instance.speedMultiplier;
        obstacle.ObstacleUpdate += Move;

        if(telegraphs)
        {
            telegraph.transform.localScale = new Vector3(1, 1, 0);
        }


    }

    public void Move()
    {
        if (!obstacle.active) return;


        if (telegraphs && !telegraphDeployed)
        {
            telegraphDeployed = true;
            telegraph.transform.localScale = new Vector3(1, 1, 0);
            telegraph.transform.DOScale(Vector3.one, telegraphSpeed);
        }

        currentSpeed = baseSpeed * GameManager.Instance.speedMultiplier;
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public void Deactivate()
    {
        if (telegraphs)
        {
            telegraph.transform.localScale = new Vector3(1, 1, 0);
        }

        obstacle.ObstacleUpdate -= Move;
    }
}
