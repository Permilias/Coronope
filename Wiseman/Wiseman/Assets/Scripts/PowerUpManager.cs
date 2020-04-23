using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    int carrotsGotten;

    public void PowerUp(PowerUpType type, Transform collectibleTransform)
    {
        string message = "";
        switch(type)
        {
            case PowerUpType.heal:
                message = "Healed 2 !";
                InfectionManager.Instance.GainInfection(-2);
                break;
            case PowerUpType.gainHp:
                message = "Gained HP !";
                InfectionManager.Instance.maxInfection++;
                InfectionWheel.Instance.GainWedge();
                break;
            case PowerUpType.slow:
                message = "Everybody slows !";
                GameManager.Instance.ResetSpeed();
                break;
            case PowerUpType.speed:
                carrotsGotten++;

                message = "Gained speed !";

                if(carrotsGotten < 4)
                {
                    PlayerController.Instance.accelerationSpeed += bonusSpeed;
                }

                break;
            case PowerUpType.super:
                
                break;
            case PowerUpType.strength:
                message = "Carry masks easier !";

                break;
        }

        FXPlayer.Instance.PlayTextMessage(collectibleTransform, Color.white, message, 3f);
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
    super,
    strength
}
