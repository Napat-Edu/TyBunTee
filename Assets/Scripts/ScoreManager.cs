using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int GetPlayerScore(int index)
    {
        return PlayerPrefs.GetInt("PlayerScore" + index, 0);
    }

    public void UpdatePlayerScore(int index, int additionalScore)
    {
        int oldScore = GetPlayerScore(index);
        PlayerPrefs.SetInt("PlayerScore" + index, oldScore + additionalScore);
    }

    public void EndMiniGame()
    {
        //
    }

    public void RestePlayerScore()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("PlayerScore" + i, 0);
        }
    }
}
