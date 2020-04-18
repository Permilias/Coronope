using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public bool randomizeRotation;
    public Vector2 minMaxRotation;
    public bool symmetry;

    public bool active;

    ObstacleMovement movement;
    ObstacleInfection infection;
    ObstacleSneezing sneezing;

    public bool scores;
    bool gaveScore;

    Vector3 basePos;

    private void Awake()
    {
        movement = GetComponent<ObstacleMovement>();

        infection = GetComponent<ObstacleInfection>();

        sneezing = GetComponent<ObstacleSneezing>();

        ObstacleUpdate += MainUpdate;

        GetBaseInfo();
    }
    public void GetBaseInfo()
    {
        basePos = transform.localPosition;
    }

    public void Initialize()
    {
        if(scores)
        {
            gaveScore = false;
        }

        transform.localPosition = basePos;
        active = false;
        if (randomizeRotation)
        {
            transform.eulerAngles = new Vector3(0, Random.Range(minMaxRotation.x, minMaxRotation.y), 0);
        }

        if (symmetry)
        {
            if (transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (movement)
        {
            movement.Initialize(this);
        }

        if (infection)
        {
            infection.Initialize();
        }

        if(sneezing)
        {
            print("sneezing");
            sneezing.Initialize(this);
        }
    }

    public void Deactivate()
    {
        if (movement)
        {
            movement.Deactivate();
        }

        if (infection)
        {
            infection.Deactivate();
        }

        if(sneezing)
        {
            sneezing.Deactivate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Collide(collision.GetContact(0));
            if (infection) infection.GetHit();


        }
    }

    public void Collide(ContactPoint point)
    {
        print("colliding !");
        Vector3 vel = point.normal * -12;
        float xMult = 2.3f - Mathf.Abs(point.normal.x);
        Mathf.Clamp(xMult, 1f, 2.3f);
        vel.x *= xMult;
        if (vel.x == 0) vel.x = Random.Range(1f, -1f);
        PlayerController.Instance.currentVel = vel;
    }

    private void Update()
    {
        ObstacleUpdate.Invoke();
    }

    public UnityAction ObstacleUpdate;

    public void MainUpdate()
    {
        if (!active)
        {
            if (transform.position.z <= ChunkManager.Instance.obstacleActivationDistance)
            {
                active = true;
            }

            return;
        }

        if (!scores) return;

        if (gaveScore) return;

        if(transform.position.z < PlayerController.Instance.transform.position.z)
        {
            gaveScore = true;
            ScoreManager.Instance.GainLivesSaved(1);
        }
    }
}
