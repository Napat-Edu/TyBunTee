using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fall_Color : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [Header("UI")]
    [SerializeField] private Image imageColor;
    [SerializeField] private Image[] imageColorList;
    private int current = 0;
    private List<int> mapColor = new List<int>(0);
    private List<int> inGame = new List<int>(0);

    void Start()
    {
        for (int i = 0; i < imageColorList.Length; i++)
        {
            if (i < colors.Count)
                imageColorList[i].color = colors[i];
        }
    }

    public void Init(int size)
    {
        List<Color> colorsList = new List<Color>(colors);

        while (colorsList.Count > 0)
        {
            int rand = Random.Range(0, colorsList.Count);
            int index = colors.IndexOf(colorsList[rand]);

            mapColor.Add(index);
            colorsList.RemoveAt(rand);
        }

        while (mapColor.Count < size)
        {
            int rand = Random.Range(0, colors.Count);
            int pos = Random.Range(0, mapColor.Count);
            mapColor.Insert(pos, rand);
        }
    }

    public void RandomColor()
    {
        List<Color> colorsList = new List<Color>(colors);
        while (colorsList.Count > 0)
        {
            int rand = Random.Range(0, colorsList.Count);
            int index = colors.IndexOf(colorsList[rand]);

            inGame.Add(index);
            colorsList.RemoveAt(rand);
        }

        current = 0;
    }

    public Color GetMapColor()
    {
        if (current >= mapColor.Count) current = 0;

        return colors[mapColor[current++]];
    }

    public Color GetCurrent()
    {
        if (current >= inGame.Count) current = 0;

        return colors[inGame[current]];
    }

    public void Next()
    {
        current++;
    }

    public void ShowColor(int addition)
    {

        int newValue = inGame[current] + addition;
        if (newValue >= colors.Count)
            newValue = newValue -  colors.Count;
        else if (newValue < 0)
            newValue = colors.Count + newValue;

        imageColor.color = colors[newValue];
        imageColor.gameObject.SetActive(true);
    }

    public void HideColor()
    {
        imageColor.gameObject.SetActive(false);
    }
}
