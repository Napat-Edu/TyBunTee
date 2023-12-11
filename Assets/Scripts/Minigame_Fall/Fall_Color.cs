using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fall_Color : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [Header("UI")]
    [SerializeField] private Image imageColor;
    [SerializeField] private Image[] imageColorList;
    private int current = 0;
    private List<int> inGame = new List<int>(0);

    void Start()
    {
        for (int i = 0; i < imageColorList.Length; i++)
        {
            if (i < colors.Length)
                imageColorList[i].color = colors[i];
        }
    }

    public Color GetInitRandom()
    {
        int rand = Random.Range(0, colors.Length);
        while (inGame.Contains(rand) && inGame.Count < colors.Length)
        {
            rand = Random.Range(0, colors.Length);
        }
        inGame.Add(rand);

        return colors[rand];
    }

    public Color GetCurrent(int addition)
    {
        current = current + addition;
        if (current >= colors.Length)
            current = current - 9;
        else if (current < 0)
            current = colors.Length + current;

        return colors[current];
    }

    public int Length
    {
        get
        {
            return inGame.Count;
        }
    }

    public bool CheckColor(int addition)
    {
        int newValue = current + addition;
        if (newValue > colors.Length)
            newValue = newValue - 8;
        else if (newValue < 0)
            newValue = colors.Length + newValue;

        return inGame.Contains(newValue);
    }

    public void RandomColor()
    {
        int rand = Random.Range(0, inGame.Count);
        current = inGame[rand];

        imageColor.color = colors[current];
        imageColor.gameObject.SetActive(true);
    }

    public void HideColor()
    {
        imageColor.gameObject.SetActive(false);
    }

    public void RemoveCurrent()
    {
        if (inGame.Contains(current))
            inGame.Remove(current);
    }
}
