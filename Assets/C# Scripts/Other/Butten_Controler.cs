using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Butten_Controler : MonoBehaviour {

    static public float difficulty = 1f;
    public bool OnHighscoreTable; //the code used to save data to the list is used in multiple scenes, therefore there score table can only be updated if you are on the high score table, therefore a public bool is used to show this.
    [Space]
    public GameObject game;
    public GameObject gameUI;
    public GameObject EndGameUI;
    public GameObject ScoreInputUI;
    [Space]
    public TextMeshProUGUI ScoreDesctiption;
    public TextMeshProUGUI ScoreNotValid;
    [Space]
    public Button SaveScore_btn;
    public TMP_InputField InputName;
    [Space]
    public PlayerController PlayerController;
    public Scoreboard Scoreboard;
    [Space]
    private ScoreboardEntryData NewScore;
    [Space]
    public Canvas Scoreboard_Easy;
    public Canvas Scoreboard_Medium;
    public Canvas Scoreboard_Hard;

    private void Awake()
    {
        if (OnHighscoreTable) //if on the high score screen
        {
            Scoreboard_Easy.enabled = false;
            Scoreboard_Medium.enabled = true;
            Scoreboard_Hard.enabled = false;
        }
    }

    public void MainMenu() //when button pressed this function is called
    {
        SceneManager.LoadScene(0); //Scene is changed - Main Menu
        Debug.Log("Back Button Pressed");
    }

    public void Options()
    {
        SceneManager.LoadScene(1); //Scene is changed - Options Menu
        Debug.Log("Options Button Pressed");
    }

    public void Score()
    {
        SceneManager.LoadScene(2);  //Scene is changed - Score Board
        Debug.Log("Score Button Pressed");
    }

    public void Play()
    {
        SceneManager.LoadScene(3);  //Scene is changed - Main Game
        Debug.Log("Play Button Pressed");
    }

    public void Easy()
    {
        difficulty = 0.5f; //the value is changed
        Debug.Log("Easy Button Pressed");
        Debug.Log(difficulty);
    }

    public void Medium()
    {
        difficulty = 1f; //the value is changed
        Debug.Log("Medium Button Pressed");
        Debug.Log(difficulty);
    }

    public void Hard()
    {
        difficulty = 2f; //the value is changed
        Debug.Log("Hard Button Pressed");
        Debug.Log(difficulty);
    }

    public void Scores_Easy()
    {
        Scoreboard_Easy.enabled = true;
        Scoreboard_Medium.enabled = false;
        Scoreboard_Hard.enabled = false;
    }

    public void Scores_Medium()
    {
        Scoreboard_Easy.enabled = false;
        Scoreboard_Medium.enabled = true;
        Scoreboard_Hard.enabled = false;
    }

    public void Scores_Hard()
    {
        Scoreboard_Easy.enabled = false;
        Scoreboard_Medium.enabled = false;
        Scoreboard_Hard.enabled = true;
    }

    public void Quit()
    {
        Application.Quit(); //End the program
        Debug.Log("Quit Button Pressed");
    }

    public void SaveScoreScreen()
    {
        game.SetActive(false); //activate and deactivate objects to create the screan
        gameUI.SetActive(false);
        EndGameUI.SetActive(false);

        ScoreInputUI.SetActive(true);

        ScoreDesctiption.enabled = true;
        ScoreNotValid.enabled = false;
    }

    public void SaveScore()
    {
        ScoreDesctiption.enabled = false;

        string value = InputName.text;

        if (value.Length != 3)
        {
            ScoreNotValid.enabled = true;
        }
        else
        {
            SaveScore_btn.enabled = false;

            NewScore.entryScore = PlayerController.score;
            NewScore.entryName = InputName.text.ToUpper();

            if(difficulty == 0.5f)
            {
                Scoreboard.AddEntry_E(NewScore);
            }
            else if(difficulty == 1f)
            {
                Scoreboard.AddEntry_M(NewScore);
            }
            else if(difficulty == 2f)
            {
                Scoreboard.AddEntry_H(NewScore);
            }
            
            SceneManager.LoadScene(0);
        }
    }
}
