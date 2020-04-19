using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnim_StartMenu : CanvasAnim
{
    public static CanvasAnim_StartMenu Instance;

    private void Awake()
    {
        Instance = this;
    }
}
