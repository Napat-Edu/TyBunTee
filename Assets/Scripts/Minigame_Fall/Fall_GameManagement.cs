using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall_GameManagement : MonoBehaviour
{
    public static bool isPlaying = false;
    public static bool isGameEnd = false;
    [SerializeField] private int gameLevel = 0;
    [SerializeField] private Fall_MapManagement mapManagement;
    [SerializeField] private Fall_BotManagement botManagement;
    [SerializeField] private Fall_Color colorManagement;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textQuestion;
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private GameObject panelGameOver;

    [SerializeField] private string nextScene;
    private List<int> oper;
    private List<int> option;
    private int time = 0;

    void Start()
    {
        InitGame();

        StartGame();
    }

    void Update()
    {
        if (isGameEnd)
        {
            if (Fall_Player.main == null)
            {
                panelGameOver.SetActive(true);
            }
            else if (botManagement.Count == 0)
            {
                panelEndGame.SetActive(true);
            }
        }
    }

    void InitGame()
    {

        isPlaying = false;
        isGameEnd = false;

        switch (gameLevel)
        {
            case 0:
                mapManagement.CreateEasyMap();
                botManagement.CreateBot(2);
                break;
            case 1:
                mapManagement.CreateNormalMap();
                botManagement.CreateBot(3);
                break;
            case 2:
                mapManagement.CreateHardMap();
                botManagement.CreateBot(4);
                break;
        }

        Fall_Player.main.StepOn(mapManagement.GetMapNoPlayer());
    }

    public void StartGame()
    {
        if (isGameEnd) return;

        isPlaying = true;

        colorManagement.RandomColor();

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
        while (time > 0)
        {
            botManagement.BotMove();
            yield return new WaitForSeconds(1f);
            time--;
            textTime.text = "Time: " + time;

            if (gameLevel == 1 && time == 5)
            {
                colorManagement.HideColor();
                textQuestion.gameObject.SetActive(false);
            }
            else if (gameLevel == 2 && time == 7)
            {
                colorManagement.HideColor();
                textQuestion.gameObject.SetActive(false);
            }
        }
        isPlaying = false;
        Fall_Player.main.Think();
        mapManagement.DestroyMap(calculate());

        yield return new WaitForSeconds(5f);

        StartGame();
    }

    public void GenerateQuestion(int count)
    {
        if (colorManagement.Length < 3)
        {
            oper = null;
            option = null;
            return;
        }

        string question;
        do
        {
            question = "";

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
        } while (!colorManagement.CheckColor(calculate()));

        textQuestion.gameObject.SetActive(true);
        textQuestion.text = question;
    }

    int calculate()
    {
        int addition = 0;
        if (oper != null)
        {
            for (int i = 0; i < oper.Count; i++)
            {
                switch (oper[i])
                {
                    case 0:
                        addition += option[i];
                        break;
                    case 1:
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
