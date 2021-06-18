using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;

    [SerializeField] private Canvas endScreen;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text subTitle;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetScoreDisplay(int score) //Sets display as parameter
    {
        scoreDisplay.SetText(score.ToString());
    }

    public void ShowEndScreen() //Shows info about reaching top score AND SAVES SCORE IF IT WAS REACHED
    {
        float topScore = PlayerPrefs.GetInt("Top score",0);
        int currentScore = gameManager.currentScore;
        if (topScore < currentScore)
        {
            title.SetText("Congratulation! You beat your top Score");
            PlayerPrefs.SetInt("Top score", currentScore);

        }else if(topScore == currentScore)
        {
            title.SetText("You reached your top score!");
        }else
        {
            title.SetText("Keep trying!");
        }
        subTitle.SetText("Current top score: " + PlayerPrefs.GetInt("Top score"));
        endScreen.gameObject.SetActive(true);
    }

    public void HideEndScreen()
    {
        endScreen.gameObject.SetActive(false);
    }
}
