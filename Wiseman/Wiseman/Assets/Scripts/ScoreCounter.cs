using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    private void Awake()
    {
        Instance = this;
    }

    public double displayedScore;
    public double targetScore;
    public TextMeshProUGUI scoreTextMesh;

    private void Update()
    {
        UpdateScoreCounter();   
    }

    public void UpdateTargetScore()
    {
        targetScore = ScoreManager.Instance.livesSaved;
    }

    public int addingDivider;
    public void UpdateScoreCounter()
    {
        int added = Mathf.RoundToInt(ScoreManager.Instance.currentGain / addingDivider);
        if (added < 1) added = 1;
        displayedScore += added;
        if (displayedScore >= targetScore) displayedScore = targetScore;

        string newText = "";

        string rawNumber = displayedScore.ToString();
        char[] charArray = rawNumber.ToCharArray();

        for(int i = charArray.Length-1; i >= 0; i--)
        {
            if(i == 2 || i == 5 ||i == 8)
            {
                newText += " ";
            }

            newText += charArray[charArray.Length-1 - i];

        }

        scoreTextMesh.text = newText;
    }
}
