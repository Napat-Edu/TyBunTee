using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

        bool currect = itemSlot.item.GetText().Trim() == answer.Trim();

        if (currect)
        {
            print("Correct");
            panelEndGame.SetActive(true);
            // ถูกทำตรงนี้
        }
        else
        {
            print("Incorrect");
            panelGameOver.SetActive(true);

            // ผิดทำตรงนี้
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void RandomScore()
    {
        // Generate random score between -1 and 2
        int randomScore = Random.Range(1, 5);
        Debug.Log("Random score: " + randomScore);
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