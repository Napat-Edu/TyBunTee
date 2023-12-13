using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 10;
    bool isCounting = true;

    // Update is called once per frame
    void Update()
    {
        if (isCounting && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Extract seconds and milliseconds
            int seconds = Mathf.FloorToInt(remainingTime);
            int milliseconds = Mathf.FloorToInt((remainingTime - seconds) * 1000);

            timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds / 10);
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //isCounting  = true;
            timerText.color = Color.red;
            timerText.text = "00:00"; // Display 00:00 when time's up
        }
    }

    public void StopTimer()
    {
        isCounting = false; // This method stops the timer countdown
        RandomScore();
    }

    public void RandomScore()
    {
        // Generate random score between -1 and 2
        int randomScore = Random.Range(-1, 3);
        Debug.Log("Random score: " + randomScore);
    }
}
