using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombTimer : MonoBehaviour
{
    [SerializeField] private Image clock;

    public bool isStopped;

    private float lifetime;
    private float timePassed;
    private GameManager gameManager;
    private SpawnManager spawnManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager == null)
            Debug.LogWarning("No game manager");
        spawnManager = FindObjectOfType<SpawnManager>();
        
        timePassed = 0;
        clock.fillAmount = 0;
        lifetime = gameManager.GetBombLifetime();
    }



    private void Update()
    {
        if (isStopped) //Do nothing when timer is stopped
            return;
        timePassed += Time.deltaTime;

        float timeRatio = timePassed / lifetime;

        clock.fillAmount = timeRatio;

        if(timeRatio >= 1)
        {

            spawnManager.SpawnBoomParticles(this.transform.position);
            gameManager.EndGame();
        }
    }
}
