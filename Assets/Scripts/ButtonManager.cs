using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadTrainScene()
    {
        SceneManager.LoadScene("Minigame_Train");
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void LoadTitleScene()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.ResetPlayerScore();
        SceneManager.LoadScene("Title");
    }

    public void ExitGame()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.ResetPlayerScore();
        Application.Quit();
    }
}
