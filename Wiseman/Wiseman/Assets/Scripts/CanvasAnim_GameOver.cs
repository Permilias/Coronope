using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnim_GameOver : CanvasAnim
{
    public static CanvasAnim_GameOver Instance;

    private void Awake()
    {
        Instance = this;
    }
}
