using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "Difficulty Config", menuName = "Configs/Difficulty", order = 5)]
public class DifficultyConfig : ScriptableObject
{
    public float addedSmoothTimePerItem;
    public float addedSpeedPerItem;
    public float maxSmoothTime;
    public float maxSpeed;

    public float maxAcceleration;
    public float accelerationGrowingDuration;
    public float accelerationGrowingStart;
}
