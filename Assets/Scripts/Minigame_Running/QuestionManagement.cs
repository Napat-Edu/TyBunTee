using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class QuestionManagement : MonoBehaviour
{
    [SerializeField] private Question[] questions;

    Question currentQuestion = null;

    [SerializeField] private DraggableItem[] answerTexts;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private GameObject panelEndGame;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private string nextScene;
    [SerializeField] private ScoreManager scoreManager;

    public string usedQuestions = "";

    void Start()
    {
        RandomQuestion();
    }

    public void RandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Length);

        while (usedQuestions.Contains(randomIndex.ToString()))
        {
            randomIndex = Random.Range(0, questions.Length);
        }
        currentQuestion = questions[randomIndex];
        usedQuestions += randomIndex.ToString();

        questionText.text = currentQuestion.question;

        // Randomize answers
        List<string> answers = new List<string>(currentQuestion.answers);
        List<string> randomizedAnswers = new List<string>();
        while (answers.Count > 0)
        {
            int randomIndex2 = Random.Range(0, answers.Count);
            randomizedAnswers.Add(answers[randomIndex2]);
            answers.RemoveAt(randomIndex2);
        }

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].SetText(randomizedAnswers[i]);
        }
    }

    public void CheckAnswer()
    {
        int index = currentQuestion.correctAnswer;
        string answer = currentQuestion.answers[index];

        bool correct = itemSlot.item.GetText().Trim() == answer.Trim();

        if (correct)
        {
            print("Correct");
            panelEndGame.SetActive(true);
            WinScore();

        }
        else
        {
            print("Incorrect");
            panelGameOver.SetActive(true);
            LoseScore();
        }
    }

    public void WinScore()
    {
        List<int> list = new List<int>() { 3, 2, 1 };
        int[] scores = new int[] { 4, 0, 0, 0 };

        for (int i = 1; i < 4; i++)
        {
            int rand = Random.Range(0, list.Count);
            scores[i] = list[rand];
            list.RemoveAt(rand);
        }

        scoreManager.UpdatePlayerScore(scores[0], scores[1], scores[2], scores[3]);
    }

    public void LoseScore()
    {
        int[] scores = new int[] { 0, 0, 0, 0 };

        for (int i = 1; i < 4; i++)
        {
            scores[i] = Random.Range(0, 5);
        }

        scoreManager.UpdatePlayerScore(scores[0], scores[1], scores[2], scores[3]);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

}


[System.Serializable]
public class Question
{
    [TextArea]
    public string question;
    public string[] answers;
    public int correctAnswer;
}