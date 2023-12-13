using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManagement : MonoBehaviour
{
    [SerializeField] private Question[] questions;

    private List<Question> usedQuestions = new List<Question>();

    [SerializeField] private DraggableItem[] answerTexts;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private ItemSlot itemSlot;

    void Start()
    {
        RandomQuestion();
    }

    void Update()
    {

    }

    public void RandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Length);

        while (usedQuestions.Contains(questions[randomIndex]))
        {
            randomIndex = Random.Range(0, questions.Length);
        }
        Question question = questions[randomIndex];

        usedQuestions.Add(question);

        questionText.text = question.question;

        // Randomize answers
        List<string> answers = new List<string>(question.answers);
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
        if (usedQuestions.Count == 0)
        {
            return;
        }

        Question question = questions[usedQuestions.Count - 1];
        int index = question.correctAnswer;

        bool currect = itemSlot.item.GetText() == question.answers[index];

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