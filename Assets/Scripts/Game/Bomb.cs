using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isBlocked;

    private SoundManager soundManager;
    private GameManager gameManager;
    private SpawnManager spawnManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    

    private void OnMouseDown()
    {
        if (isBlocked) //if blocked do nothing
            return;
        gameManager.IncreaseScore();
        soundManager.PlaySound(SoundType.CLICK);

        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        spawnManager.RemoveFromList(this.transform.position);
    }
}
