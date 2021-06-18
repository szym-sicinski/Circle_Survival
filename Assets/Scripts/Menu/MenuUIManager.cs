using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text topScoreDisplay;

    private SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        int score = PlayerPrefs.GetInt("Top score", 0);
        topScoreDisplay.SetText((score != 0) ? "Current top score: " + score : "No current top score"); //if there is top score sets that to display, else sets information that there is no top score  
    }

    public void PlayClick()
    {
        soundManager.PlaySound(SoundType.CLICK);
        SceneManager.LoadScene(1);
    }
}
