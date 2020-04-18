using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public double livesSaved;

    public int currentGain;

    public float resfreshingMultiplier;

    public int maxGain;

    public double maxLivesSaved;

    public void GainLivesSaved(int multiplier)
    {
        livesSaved += currentGain * multiplier;
        if (livesSaved >= maxLivesSaved) livesSaved = maxLivesSaved;

        ScoreCounter.Instance.UpdateTargetScore();
        RefreshCurrentGain();
    }

    public void LooseLivesSaved(int amount)
    {
        livesSaved--;
        if (livesSaved <= 0) livesSaved = 0;
        ScoreCounter.Instance.UpdateTargetScore();

    }

    void RefreshCurrentGain()
    {
        int previous = currentGain;

        currentGain = Mathf.RoundToInt(currentGain * resfreshingMultiplier);

        if (currentGain <= previous) currentGain++;

        if (currentGain >= maxGain) currentGain = maxGain;
    }

    public void ResetCurrentGain()
    {
        currentGain = 1;
    }
}
