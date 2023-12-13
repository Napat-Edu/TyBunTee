using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManagement : MonoBehaviour
{
    [SerializeField] private Question[] questions;

    Question currentQuestion = null;

    [SerializeField] private DraggableItem[] answerTexts;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private ItemSlot itemSlot;

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

            // ถูกทำตรงนี้
        }
        else
        {
            print("Incorrect");

            // ผิดทำตรงนี้
        }
    }
}


[System.Serializable]
public class Question
{
    public string question;
    public string[] answers;
    public int correctAnswer;
}