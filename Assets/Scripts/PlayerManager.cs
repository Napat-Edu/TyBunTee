using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerScoreText;
    [SerializeField] TextMeshProUGUI difficulty;
    [SerializeField] TextMeshProUGUI topScoreText;

    [SerializeField] ScoreManager scoreManager;

    [SerializeField] GameObject EndgamePopup;

    int[] playerScore;

    void Awake()
    {
        playerScore = new int[playerScoreText.Length];

        InitPlayerScore();
        scoreManager.CountTurn();

        difficulty.text = SetDifficultText();
        CheckEndgameCondition();
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

    void CheckEndgameCondition()
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
    }

    void OpenEndGameDiaogue(int topScore)
    {
        topScoreText.text = "Top Score : " + topScore;
        EndgamePopup.SetActive(true);
    }
}
