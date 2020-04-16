using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleMovement : MonoBehaviour
{
    public float baseSpeed;
    float currentSpeed;
    Obstacle obstacle;

    public void Initialize(Obstacle _obstacle)
    {
        obstacle = _obstacle;
        currentSpeed = baseSpeed * GameManager.Instance.speedMultiplier;
        obstacle.ObstacleUpdate += Move;
    }

    public void Move()
    {
        if (!obstacle.active) return;
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public void Deactivate()
    {
        obstacle.ObstacleUpdate -= Move;
    }
}
