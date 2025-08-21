using System.IO;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10; //max number of scores in list
    public bool OnHighscoreTable; //the code used to save data to the list is used in multiple scenes, therefore there score table can only be updated if you are on the high score table, therefore a public bool is used to show this.
    [Space]
    [Header("EASY")]
    [SerializeField] private Transform highscoresHolderTranform_E; //location of where the scores should be instantiated
    [SerializeField] private GameObject scoreboardEntryObject_E; //object to instantiate objects into

    [Space]
    [Header("MEDIUM")]
    [SerializeField] private Transform highscoresHolderTranform_M; //location of where the scores should be instantiated
    [SerializeField] private GameObject scoreboardEntryObject_M; //object to instantiate objects into

    [Space]
    [Header("HARD")]
    [SerializeField] private Transform highscoresHolderTranform_H; //location of where the scores should be instantiated
    [SerializeField] private GameObject scoreboardEntryObject_H; //object to instantiate objects into

    private string SavePath_E => $"{Application.persistentDataPath}/Highscores_E.json"; //Save location of the text file
    private string SavePath_M => $"{Application.persistentDataPath}/Highscores_M.json"; //Save location of the text file
    private string SavePath_H => $"{Application.persistentDataPath}/Highscores_H.json"; //Save location of the text file

    private void Awake() //runs when the scoreboard scene is opened
    {
        if (OnHighscoreTable)
        {
            ScoreboardSaveData savedScores_E = GetSavedScores_E();
            ScoreboardSaveData savedScores_M = GetSavedScores_M();
            ScoreboardSaveData savedScores_H = GetSavedScores_H();

            UpdateUI_E(savedScores_E);
            UpdateUI_M(savedScores_M);
            UpdateUI_H(savedScores_H);
        }
    }

    public void AddEntry_E(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores_E();

        bool scoreAdded = false; // does the item fit in the list

        for (int i = 0; i < savedScores.highscores.Count; i++) //repeat for the length of the list
        {
            if(scoreAdded)
            {
                break;//stop comparing values
            }

            if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore) //if bigger any value - starting from the first one
            {
                savedScores.highscores.Insert(i, scoreboardEntryData); //insert it in the list at the right place
                scoreAdded = true; //not that the value is in the list
            }
        }

        if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries) //if there is less then the max values and the value is not already added, add value to the end of the list
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        if (savedScores.highscores.Count > maxScoreboardEntries) //if there are too many items in the list, remove the extra items
        {
            savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
        }

        SaveScores_E(savedScores); //save the new list to the text file

        if (OnHighscoreTable) //if on the high score screen
        {
            UpdateUI_E(savedScores); //load and update the table
        }
    }

    public void AddEntry_M(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores_M();

        bool scoreAdded = false; // does the item fit in the list

        for (int i = 0; i < savedScores.highscores.Count; i++) //repeat for the length of the list
        {
            if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore) //if bigger any value - starting from the first one
            {
                savedScores.highscores.Insert(i, scoreboardEntryData); //insert it in the list at the right place
                scoreAdded = true; //not that the value is in the list
                break; //stop comparing values
            }
        }

        if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries) //if there is less then the max values and the value is not already added, add value to the end of the list
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        if (savedScores.highscores.Count > maxScoreboardEntries) //if there are too many items in the list, remove the extra items
        {
            savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
        }

        SaveScores_M(savedScores); //save the new list to the text file

        if (OnHighscoreTable) //if on the high score screen
        {
            UpdateUI_M(savedScores); //load and update the table
        }
    }

    public void AddEntry_H(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores_H();

        bool scoreAdded = false; // does the item fit in the list

        for (int i = 0; i < savedScores.highscores.Count; i++) //repeat for the length of the list
        {
            if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore) //if bigger any value - starting from the first one
            {
                savedScores.highscores.Insert(i, scoreboardEntryData); //insert it in the list at the right place
                scoreAdded = true; //not that the value is in the list
                break; //stop comparing values
            }
        }

        if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries) //if there is less then the max values and the value is not already added, add value to the end of the list
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        if (savedScores.highscores.Count > maxScoreboardEntries) //if there are too many items in the list, remove the extra items
        {
            savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
        }

        SaveScores_H(savedScores); //save the new list to the text file

        if (OnHighscoreTable) //if on the high score screen
        {
            UpdateUI_H(savedScores); //load and update the table
        }
    }

    private void UpdateUI_E(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in highscoresHolderTranform_E)
        {
            Destroy(child.gameObject); //remove the existing values
        }

        foreach (ScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject_E, highscoresHolderTranform_E).GetComponent<ScoreboardEntryUI>().Initialise(highscore); //instantiate objects to values to the list
        }
    }

    private void UpdateUI_M(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in highscoresHolderTranform_M)
        {
            Destroy(child.gameObject); //remove the existing values
        }

        foreach (ScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject_M, highscoresHolderTranform_M).GetComponent<ScoreboardEntryUI>().Initialise(highscore); //instantiate objects to values to the list
        }
    }

    private void UpdateUI_H(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in highscoresHolderTranform_H)
        {
            Destroy(child.gameObject); //remove the existing values
        }

        foreach (ScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject_H, highscoresHolderTranform_H).GetComponent<ScoreboardEntryUI>().Initialise(highscore); //instantiate objects to values to the list
        }
    }

    private ScoreboardSaveData GetSavedScores_E() //to save data
    {
        if (!File.Exists(SavePath_E)) //if there is no existing table
        {
            File.Create(SavePath_E).Dispose(); //create a file
            return new ScoreboardSaveData(); //and add return the empty list
        }

        using (StreamReader stream = new StreamReader(SavePath_E)) //reading from the list
        {
            string json = stream.ReadToEnd(); //read the values into a string ‘json’

            return JsonUtility.FromJson<ScoreboardSaveData>(json); //return values from the list
        }
    }

    private ScoreboardSaveData GetSavedScores_M() //to save data
    {
        if (!File.Exists(SavePath_M)) //if there is no existing table
        {
            File.Create(SavePath_M).Dispose(); //create a file
            return new ScoreboardSaveData(); //and add return the empty list
        }

        using (StreamReader stream = new StreamReader(SavePath_M)) //reading from the list
        {
            string json = stream.ReadToEnd(); //read the values into a string ‘json’

            return JsonUtility.FromJson<ScoreboardSaveData>(json); //return values from the list
        }
    }

    private ScoreboardSaveData GetSavedScores_H() //to save data
    {
        if (!File.Exists(SavePath_H)) //if there is no existing table
        {
            File.Create(SavePath_H).Dispose(); //create a file
            return new ScoreboardSaveData(); //and add return the empty list
        }

        using (StreamReader stream = new StreamReader(SavePath_H)) //reading from the list
        {
            string json = stream.ReadToEnd(); //read the values into a string ‘json’

            return JsonUtility.FromJson<ScoreboardSaveData>(json); //return values from the list
        }
    }

    private void SaveScores_E(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath_E)) //Writing a sting
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true); //link the values to write into a string json
            stream.Write(json); //write values into string
        }
    }

    private void SaveScores_M(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath_M)) //Writing a sting
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true); //link the values to write into a string json
            stream.Write(json); //write values into string
        }
    }

    private void SaveScores_H(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath_H)) //Writing a sting
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true); //link the values to write into a string json
            stream.Write(json); //write values into string
        }
    }
}
