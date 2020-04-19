using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnim_Game : CanvasAnim
{
    public static CanvasAnim_Game Instance;

    private void Awake()
    {
        Instance = this;
    }


}
