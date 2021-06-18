using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    private TMP_Text timerDisplay;

    private float secondsPassed;

    public bool isStopped; //If true timer will be stopped

    private void Start()
    {
        timerDisplay = GetComponent<TMP_Text>();
        DisplayScore();
    }

    private void Update()
    {
        if(!isStopped)
        {
            secondsPassed += Time.deltaTime; //I will not change time scale so I can use that instead of unscaledDeltaTime
            DisplayScore();
        }
    }


    private void DisplayScore() //Updates timer display in format mm:ss
    {
        timerDisplay.SetText(string.Format("{0:00}:{1:00}", Mathf.FloorToInt(secondsPassed / 60), Mathf.FloorToInt(secondsPassed % 60)));
    }

    public void ResetTimer()
    {
        isStopped = false;
        secondsPassed = 0;
        DisplayScore();
    }
}
