using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will store score and lifetime of bombs
public class GameManager : MonoBehaviour 
{
    public int currentScore;
    

    private Timer timer; //reference to game timer script

    private float minBombLifetime = 2;
    private float maxBombLifetime = 4; //Min and max seconds of life a bomb
    private const int BOMBS_TO_LEVEL_UP = 8; //when players disables this number of bombs difficulty will increase 
    private int spawnedBombsCounter; //used to check if it's time to increase difficulty
    private GameUIManager gameUIManager;
    private SpawnManager spawnManager;
    private SoundManager soundManager;
    
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        gameUIManager = FindObjectOfType<GameUIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        soundManager = FindObjectOfType<SoundManager>();
        gameUIManager.SetScoreDisplay(currentScore);
    }

    public void IncreaseScore()
    {
        gameUIManager.SetScoreDisplay(++currentScore); //First increments score by one then sets display value
    }

    public void ResetGame() //Sets all vars with starting values, set score display to 0
    {
        currentScore = 0;
        gameUIManager.SetScoreDisplay(currentScore);
        gameUIManager.HideEndScreen();

        minBombLifetime = 2;
        maxBombLifetime = 4;

        spawnManager.ResetValues();
        spawnManager.StartCoroutine("Start");

        timer.ResetTimer();

        foreach (Bomb bomb in FindObjectsOfType<Bomb>()) //Destroys all bombs
        {
            Destroy(bomb.gameObject);
        }
        foreach (Trap trap in FindObjectsOfType<Trap>()) //Destroys all traps
        {
            Destroy(trap.gameObject);
        }
    }

    public float GetBombLifetime() //returns random lifetime from [minBombLifetime, maxBombLifetime) and increase difficulty if enough bombs were spawned
    {
        float resultLifetime = UnityEngine.Random.Range(minBombLifetime,maxBombLifetime);

        if(++spawnedBombsCounter >= BOMBS_TO_LEVEL_UP) //resets bombs counter and increases difficulty
        {
            spawnedBombsCounter = 0;

            IncreaseDifficulty();
            spawnManager.IncreaseDifficulty();
        }

        return resultLifetime;
    }

    private void IncreaseDifficulty()//decrease lifetime of bombs by [5-10]%
    {
        float decrease = UnityEngine.Random.Range(0.9f, 0.95f);


        minBombLifetime *= decrease;
        maxBombLifetime *= decrease;

    }
    public void EndGame()
    {
        timer.isStopped = true;
        spawnManager.StopAllCoroutines(); //Stops spawner
        soundManager.PlaySound(SoundType.BOOM);
        foreach(BombTimer bombTimer in FindObjectsOfType<BombTimer>()) //Stops all bomb timers
        {
            bombTimer.isStopped = true;
        }
        foreach (Bomb bomb in FindObjectsOfType<Bomb>()) //Blocks all bobms so player cant tap them no more
        {
            bomb.isBlocked = true;
        }
        foreach (Trap trap in FindObjectsOfType<Trap>()) //Blocks all traps so player cant tap them no more
        {
            trap.isBlocked = true;
        }
        gameUIManager.ShowEndScreen();
        //throw new NotImplementedException();
    }

}
