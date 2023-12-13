using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerScoreText;
    [SerializeField] TextMeshProUGUI difficulty;

    [SerializeField] ScoreManager scoreManager;

    int[] playerScore;

    void Awake()
    {
        difficulty.text = "Difficulty : Easy";
        playerScore = new int[playerScoreText.Length];
        InitPlayerScore();
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
}
