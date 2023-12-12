using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerScore;
    [SerializeField] TextMeshProUGUI difficulty;

    void Awake()
    {
        difficulty.text = "Difficulty : Easy";
        InitPlayerScore();
    }

    void InitPlayerScore()
    {
        for (int i = 0; i < playerScore.Length; i++)
        {
            playerScore[i].text = "Score\n" + 0 + "/30";
        }
    }
}
