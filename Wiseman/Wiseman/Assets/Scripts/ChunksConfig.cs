using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunks Config", menuName = "Configs/Chunks", order = 2)]
public class ChunksConfig : ScriptableObject
{
    public float chunkSize;
    public float baseSpeed;
}
