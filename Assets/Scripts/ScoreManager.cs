using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int GetPlayerScore(int index)
    {
        return PlayerPrefs.GetInt("PlayerScore" + index, 0);
    }

    public int GetDifficultyLevel()
    {
        return PlayerPrefs.GetInt("DifficultyLevel", 0);
    }

    public int GetCurrentTurn()
    {
        return PlayerPrefs.GetInt("CurrentTurn", 0);
    }

    public void UpdatePlayerScore(int p0score, int p1score, int p2score, int p3score)
    {
        int[] scores = new int[4] { p0score, p1score, p2score, p3score };
        for (int i = 0; i < 4; i++)
        {
            int oldScore = GetPlayerScore(i);
            PlayerPrefs.SetInt("PlayerScore" + i, oldScore + scores[i]);
        }
    }

    public void ResetPlayerScoreAndDifficulty()
    {
        PlayerPrefs.SetInt("DifficultyLevel", 0);
        PlayerPrefs.SetInt("CurrentTurn", 0);
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("PlayerScore" + i, 0);
        }
    }

    public void SetDifficultLevel(int level)
    {
        if (level < 3)
        {
            PlayerPrefs.SetInt("DifficultyLevel", level);
        }
    }

    public void CountTurn()
    {
        int turn = GetCurrentTurn() + 1;
        PlayerPrefs.SetInt("CurrentTurn", turn);

        if (turn >= 4)
        {
            SetDifficultLevel(1);
        }
        else if (turn >= 8)
        {
            SetDifficultLevel(2);
        }
    }
}
