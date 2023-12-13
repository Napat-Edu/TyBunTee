using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall_GameManagement : MonoBehaviour
{
    public static bool isPlaying = false;
    public static bool isGameEnd = false;
    public static Fall_GameManagement main;
    [SerializeField] private int gameLevel = 0;
    [SerializeField] private Fall_MapManagement mapManagement;
    [SerializeField] private Fall_BotManagement botManagement;
    [SerializeField] private Fall_Color colorManagement;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textQuestion;
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private string nextScene;
    private List<int> oper;
    private List<int> option;
    private int time = 0;

    private bool hasUpdateScore = false;
    int[] score = new int[4] { 0, 0, 0, 0 };
    int maxScore = 1;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        InitGame();

        StartGame();
    }

    void Update()
    {
        if (isGameEnd && !hasUpdateScore)
        {
            if (Fall_Player.main == null)
            {
                panelGameOver.SetActive(true);
                for (int i = 0; i < 4; i++)
                {
                    if (score[i] == 0)
                    {
                        score[i] = maxScore++;
                    }
                }
            }
            else if (botManagement.Count == 0)
            {
                panelEndGame.SetActive(true);
                score[0] = maxScore++;
            }
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        hasUpdateScore = true;
        scoreManager.UpdatePlayerScore(score[0], score[1], score[2], score[3]);
    }

    public void SetScore(int index)
    {
        score[index] = maxScore++;
    }

    void InitGame()
    {
        gameLevel = scoreManager.GetDifficultyLevel();
        hasUpdateScore = false;
        isPlaying = false;
        isGameEnd = false;

        switch (gameLevel)
        {
            case 0:
                mapManagement.CreateEasyMap();
                break;
            case 1:
                mapManagement.CreateNormalMap();
                break;
            case 2:
                mapManagement.CreateHardMap();
                break;
        }

        colorManagement.RandomColor();
        botManagement.CreateBot(3);
        Fall_Player.main.StepOn(mapManagement.GetMapNoPlayer());
    }

    public void StartGame()
    {
        if (isGameEnd) return;

        isPlaying = true;

        if (gameLevel == 1)
            GenerateQuestion(1);
        else if (gameLevel == 2)
            GenerateQuestion(2);
        else
        {
            textQuestion.gameObject.SetActive(false);
            oper = null;
            option = null;
        }
        time = 10;
        textTime.text = "Time: " + time;

        StartCoroutine(GameRun());
    }

    public IEnumerator GameRun()
    {
        colorManagement.ShowColor(getAddition());
        while (time > 0)
        {
            botManagement.BotMove();
            yield return new WaitForSeconds(1f);
            time--;
            textTime.text = "Time: " + time;

            if (gameLevel == 2 && time == 7)
            {
                colorManagement.HideColor();
                textQuestion.gameObject.SetActive(false);
            }

            if (isGameEnd) yield break;
        }
        isPlaying = false;
        Fall_Player.main.Think();
        mapManagement.DestroyMap();

        yield return new WaitForSeconds(5f);
        StartGame();
    }

    public void GenerateQuestion(int count)
    {
        string question = "";

        oper = new List<int>();
        option = new List<int>();

        for (int i = 0; i < count; i++)
        {
            option.Add(Random.Range(1, 4));
            oper.Add(Random.Range(0, 2));

            switch (oper[i])
            {
                case 0:
                    question += " + " + option[i];
                    break;
                case 1:
                    question += " - " + option[i];
                    break;
            }
        }

        textQuestion.gameObject.SetActive(true);
        textQuestion.text = question;
    }

    int getAddition()
    {
        int addition = 0;
        if (oper != null)
        {
            for (int i = 0; i < oper.Count; i++)
            {
                switch (oper[i])
                {
                    case 1:
                        addition += option[i];
                        break;
                    case 0:
                        addition -= option[i];
                        break;
                }
            }
        }

        return addition;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
