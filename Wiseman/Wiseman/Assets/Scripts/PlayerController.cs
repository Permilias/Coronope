using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
    }

    public PlayerControlConfig config;

    Rigidbody rb;


    public Vector3 currentAcceleration;
    public Vector3 currentSpeed;

    Vector3 targetVel;
    public Vector3 currentVel;
    Vector3 velRef;
    private void Update()
    {
        if(stunCount > 0f)
        {
            stunCount -= Time.deltaTime;
            if(stunCount <= 0f)
            {
                Unstun();
            }
        }

        InputUpdate();

        SpeedUpdate();

        targetVel = currentSpeed;
        currentVel = Vector3.SmoothDamp(currentVel, targetVel, ref velRef, smoothTime + stunSmooth);

        rb.velocity = currentVel;

        if(transform.position.z < -1.901f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.901f);
        }
    }

    public float accelerationSpeed;
    public float smoothTime;
    public void RefreshMovementValues()
    {
        accelerationSpeed = config.inputAcceleration + (CollectibleManager.Instance.masksPossessed * GameManager.Instance.difficultyConfig.addedSpeedPerItem);
        if (accelerationSpeed >= GameManager.Instance.difficultyConfig.maxSpeed) accelerationSpeed = GameManager.Instance.difficultyConfig.maxSpeed;
        smoothTime = config.smoothTime + (CollectibleManager.Instance.masksPossessed * GameManager.Instance.difficultyConfig.addedSmoothTimePerItem);
        if (smoothTime >= GameManager.Instance.difficultyConfig.maxSmoothTime) smoothTime = GameManager.Instance.difficultyConfig.maxSmoothTime;
    }

    bool leftHeld;
    bool rightHeld;
    bool upHeld;
    bool downHeld;
    void InputUpdate()
    {
        leftHeld = false;
        for (int i = 0; i < config.leftInputs.Length; i++)
        {
            if(Input.GetKey(config.leftInputs[i]))
            {
                leftHeld = true;
                break;
            }
        }

        rightHeld = false;
        for (int i = 0; i < config.rightInputs.Length; i++)
        {
            if (Input.GetKey(config.rightInputs[i]))
            {
                rightHeld = true;
                break;
            }
        }

        upHeld = false;
        for (int i = 0; i < config.upInputs.Length; i++)
        {
            if (Input.GetKey(config.upInputs[i]))
            {
                upHeld = true;
                break;
            }
        }

        downHeld = false;
        for (int i = 0; i < config.downInputs.Length; i++)
        {
            if (Input.GetKey(config.downInputs[i]))
            {
                downHeld = true;
                break;
            }
        }
    }

    void SpeedUpdate()
    {
        currentAcceleration = Vector3.zero;

        if (leftHeld) currentAcceleration.x -= 1;
        if (rightHeld) currentAcceleration.x += 1;
        if (upHeld) currentAcceleration.z += 1;
        if (downHeld) currentAcceleration.z -= 1;

        currentSpeed = currentAcceleration;
        currentSpeed.Normalize();
        currentSpeed *= accelerationSpeed + stunSpeed;

    }

    float stunSpeed;
    float stunSmooth;
    float stunCount;

    public void Stun()
    {
        stunSpeed = config.stunSpeed;
        stunSmooth = config.stunSmooth;
        stunCount = config.stunDuration;
        PlayerAnimation.Instance.stunStars.SetActive(true);
        PlayerAnimation.Instance.graphics.Stun();
    }

    public void Unstun()
    {
        stunSpeed = 0f;
        stunSmooth = 0f;
        PlayerAnimation.Instance.stunStars.SetActive(false);
        PlayerAnimation.Instance.graphics.Unstun();
    }

}
