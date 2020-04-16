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
        InputUpdate();

        SpeedUpdate();

        targetVel = currentSpeed;
        targetVel *= Time.deltaTime;
        currentVel = Vector3.SmoothDamp(currentVel, targetVel, ref velRef, config.smoothTime);

        rb.velocity = currentVel;

        if(transform.position.z < -1.901f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.901f);
        }
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
        currentSpeed *= config.inputAcceleration;

    }
}
