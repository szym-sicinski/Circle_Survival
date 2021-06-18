using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float secondsToSpawn = 1f; //How much seconds will pass between spawns

    private const int CHANCE_TO_TRAP = 10; //% of chance to spawn trap instead of bomb

    [SerializeField] private GameObject bombPrefab;


    [SerializeField] private GameObject trapPrefab;
    private const float DISTANCE_BETWEEN_BOMBS = 1f; // Used in calculation of collision

    [SerializeField] private GameObject topLeftVertex;//
    [SerializeField] private GameObject bottomRightVertex;//Those gameobjects are placed in gameCanvas. They're defining borders of spawnable area. With this I can easyli adjust game area

    private List<Vector3> bombs = new List<Vector3>(); //List to store all bombs and traps that are now on game area

    [SerializeField] private Camera mainCamera; //Referance for camera to avoid using Camera.main.

    [SerializeField] private GameObject boomParticles;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private IEnumerator Start() //Coroutine that automatically starts. It will spawn bombs and traps every secondsTospawn seconds
    {
        for (; ; )
        {
            Vector3 spawnPos = RandomSpawnPos(); //Random spawn pos return vector zero when detected collision and we skip spawning in this iteration and we wait for secondsToSpawn / 10 instead of secondsToSpawn
            if (spawnPos != Vector3.zero)
            {
                if (UnityEngine.Random.Range(0, 101) > CHANCE_TO_TRAP) //if random number from [0,100] is greater then chance to trap then spawn bomb else spawn trap
                {
                    #if  UNITY_EDITOR
                        Debug.Log("Bomb spawned");
                    #endif
                    Instantiate(bombPrefab, spawnPos, Quaternion.identity);
                }
                else
                {
                    #if  UNITY_EDITOR
                        Debug.Log("Trap spawned");
                    #endif
                    Instantiate(trapPrefab, spawnPos, Quaternion.identity);
                }
                //bombs.Add(mainCamera.WorldToScreenPoint(spawnPos));
                bombs.Add(spawnPos);
                soundManager.PlaySound(SoundType.SPAWN);
                yield return new WaitForSeconds(secondsToSpawn);
            }
            else
            {
                yield return new WaitForSeconds(secondsToSpawn/10);
            }
        }

    }

    private Vector3 RandomSpawnPos()
    {
        float x = UnityEngine.Random.Range(topLeftVertex.transform.position.x, bottomRightVertex.transform.position.x);
        float y = UnityEngine.Random.Range(topLeftVertex.transform.position.y, bottomRightVertex.transform.position.y);

        Vector3 resultPos = new Vector3(x, y, 0); //random vector in world units in game area

        foreach (Vector2 pos in bombs) //If this loop will find collision it will return vector2.zero and Start corutine will not spawn in this iteration any bomb
        {
            //if (Vector2.Distance(pos, mainCamera.WorldToScreenPoint(resultPos)) < DISTANCE_BETWEEN_BOMBS)
            if (Vector3.Distance(pos, resultPos) < DISTANCE_BETWEEN_BOMBS)
            {
                #if UNITY_EDITOR
                    Debug.Log("Collision");
                #endif
                return Vector3.zero;
            }
        }

        return resultPos;
    }

    public void IncreaseDifficulty() //decreases secondsToSpawn by [10-15]%
    {
        secondsToSpawn *= UnityEngine.Random.Range(0.85f, 0.9f);
    }

    public void ResetValues()
    {
        secondsToSpawn = 1f;
    }

    public void RemoveFromList(Vector3 position)
    {
        bombs.Remove(position);
    }

    public void SpawnBoomParticles(Vector3 pos)
    {
        GameObject particles = Instantiate(boomParticles, pos, Quaternion.identity);
        Destroy(particles, 5f); //Particles effects lasts around 5 seconds
    }
}
