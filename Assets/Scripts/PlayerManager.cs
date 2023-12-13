using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerScoreText;
    [SerializeField] TextMeshProUGUI difficulty;
    [SerializeField] TextMeshProUGUI topScoreText;

    [SerializeField] ScoreManager scoreManager;

    [SerializeField] GameObject EndgamePopup;

    [SerializeField] Transform[] framePosition;
    [SerializeField] GameObject SelectedFrame;

    int[] playerScore;

    void Awake()
    {
        playerScore = new int[playerScoreText.Length];

        InitPlayerScore();
        scoreManager.CountTurn();

        difficulty.text = SetDifficultText();
        bool isPlayerWinExist = CheckEndgameCondition();

        if (!isPlayerWinExist)
        {
            RandomMiniGame();
        }
    }

    void InitPlayerScore()
    {
        for (int i = 0; i < playerScoreText.Length; i++)
        {
            int value = scoreManager.GetPlayerScore(i);
            playerScore[i] = value;
            playerScoreText[i].text = "Score\n" + playerScore[i] + "/30";
        }
    }

    string SetDifficultText()
    {
        string baseText = "Difficulty : ";
        int level = scoreManager.GetDifficultyLevel();

        if (level == 0)
        {
            baseText += "Easy";
        }
        else if (level == 1)
        {
            baseText += "Normal";
        }
        else if (level == 2)
        {
            baseText += "Hard";
        }

        return baseText;
    }

    bool CheckEndgameCondition()
    {
        bool isPlayerWinExist = false;
        int topScore = 0;
        for (int i = 0; i < playerScore.Length; i++)
        {
            if (playerScore[i] >= 30)
            {
                isPlayerWinExist = true;
            }
            if (playerScore[i] > topScore)
            {
                topScore = playerScore[i];
            }
        }

        if (isPlayerWinExist)
        {
            WinnerManager winnerManager = FindObjectOfType<WinnerManager>();
            for (int i = 0; i < playerScore.Length; i++)
            {
                if (playerScore[i] == topScore)
                {
                    winnerManager.OpenWinnerIcon(i);
                }
            }
            OpenEndGameDiaogue(topScore);
        }

        return isPlayerWinExist;
    }

    void OpenEndGameDiaogue(int topScore)
    {
        topScoreText.text = "Top Score : " + topScore;
        EndgamePopup.SetActive(true);
    }

    void RandomMiniGame()
    {
        int minigame = Random.Range(1, 4);

        StartCoroutine(WaitForMinigame(minigame));
    }

    IEnumerator WaitForMinigame(int minigame)
    {
        yield return new WaitForSeconds(1);

        SelectedFrame.transform.position = framePosition[minigame - 1].transform.position;
        SelectedFrame.SetActive(true);

        yield return new WaitForSeconds(2);

        if (minigame == 1)
        {
            SceneManager.LoadScene("Minigame_Train");
        }
        else if (minigame == 2)
        {
            SceneManager.LoadScene("Temp_Running");
        }
        else
        {
            SceneManager.LoadScene("Fall_Game");
        }
    }
}
