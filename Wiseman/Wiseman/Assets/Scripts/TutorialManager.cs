using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] tutorialBodies;

    public void Initialize()
    {
        DeactivateAllTutorials();
    }

    float currentDuration;
    int currentIndex;

    public void PlayTutorial(int index, float duration)
    {
        DeactivateAllTutorials();
        currentDuration = duration;
        currentIndex = index;
        tutorialBodies[currentIndex].SetActive(true);
    }

    private void Update()
    {
        if(currentDuration > 0f)
        {
            currentDuration -= Time.deltaTime;
            if(currentDuration <= 0f)
            {
                DeactivateAllTutorials();
            }
        }
    }

    public void DeactivateAllTutorials()
    {
        for (int i = 0; i < tutorialBodies.Length; i++)
        {
            tutorialBodies[i].SetActive(false);

        }
    }
}
