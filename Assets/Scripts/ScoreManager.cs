using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int GetPlayerScore(int index)
    {
        return PlayerPrefs.GetInt("PlayerScore" + index, 0);
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

    public void ResetPlayerScore()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("PlayerScore" + i, 0);
        }
    }
}
