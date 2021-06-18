using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isBlocked;
    
    private const float TRAP_LIFETIME = 3f;

    private SpawnManager spawnManager;
    private GameManager gameManager;
    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(WaitAndDisable));
    }

    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(TRAP_LIFETIME);

        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        if (isBlocked) //if blocked do nothing
            return;
        spawnManager.SpawnBoomParticles(this.transform.position);
        gameManager.EndGame();
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(WaitAndDisable));
        spawnManager.RemoveFromList(this.transform.position);
    }
}
