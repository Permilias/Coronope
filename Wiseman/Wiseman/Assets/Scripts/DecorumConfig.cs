using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decorum Config", menuName = "Configs/Decorum", order = 2)]
public class DecorumConfig : ScriptableObject
{
    public GameObject[] decorumObjects;
    public float decorumDistanceFromCenter;
    public float decorumDistanceFromEachOther;
    public int poolAmount;
}
