using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int tutorialIndex;
    public float duration;

    bool activated;

    private void Update()
    {
        if (activated) return;

        CheckActivation();
    }

    public void CheckActivation()
    {
        if(PlayerController.Instance.transform.position.z >= transform.position.z)
        {
            TutorialManager.Instance.PlayTutorial(tutorialIndex, duration);
            activated = true;
        }


    }
}
