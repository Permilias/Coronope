using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnim : MonoBehaviour
{
    public GameObject body;

    public virtual void Display()
    {
        body.SetActive(true);
    }

    public virtual void Hide()
    {
        body.SetActive(false);
    }
}
