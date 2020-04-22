using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public void PowerUp(PowerUpType type)
    {
        switch(type)
        {
            case PowerUpType.heal:
                break;
            case PowerUpType.shield:
                break;
            case PowerUpType.barrier:
                break;
            case PowerUpType.gainHp:
                break;
            case PowerUpType.slow:
                break;
            case PowerUpType.speed:
                break;
            case PowerUpType.super:
                break;
        }
    }

    public float bonusSpeed;
    public float superBonusSpeed;
    public float superDuration;
    public float smoothReduction;
    public float gameSpeedReduction;
    public float barrierDuration;

}

public enum PowerUpType
{
    heal,
    shield,
    barrier,
    gainHp,
    slow,
    speed,
    super
}
