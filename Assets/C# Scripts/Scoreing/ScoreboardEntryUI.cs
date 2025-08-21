using UnityEngine;
using TMPro;


public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void Initialise(ScoreboardEntryData ScoreboardEntryData)
    {
        entryNameText.text = ScoreboardEntryData.entryName;
        entryScoreText.text = ScoreboardEntryData.entryScore.ToString();
    }
}

