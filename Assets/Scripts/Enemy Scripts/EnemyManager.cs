using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    GameObject boarPrefab, cannibalPrefab;

    public Transform[] cannibalSpawnPoints, boarSpawnPoints;

    int cannibalCount, boarCount;

    int initialCannibalCount, initialBoarCount;
    public float enemySpawnTimer = 10f;

    List<GameObject> spawnedCannibals, spawnedBoars;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnedCannibals = new List<GameObject>();
        spawnedBoars = new List<GameObject>();
    }

    void Start()
    {
        cannibalCount = cannibalSpawnPoints.Length;
        boarCount = boarSpawnPoints.Length;

        initialCannibalCount = cannibalCount;
        initialBoarCount = boarCount;

        spawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    private void spawnEnemies()
    {
        spawnCannibals();
        spawnBoars();
    }

    private void spawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boarCount; i++)
        {
            if (index > boarSpawnPoints.Length)
            {
                index = 0;
            }

            GameObject boar = Instantiate(boarPrefab, boarSpawnPoints[index].position, Quaternion.identity) as GameObject;
            spawnedBoars.Add(boar);
            index++;
        }

        boarCount = 0;
    }

    private void spawnCannibals()
    {
        int index = 0;

        for (int i = 0; i < cannibalCount; i++)
        {
            if (index > cannibalSpawnPoints.Length)
            {
                index = 0;
            }

            GameObject cannibal = Instantiate(cannibalPrefab, cannibalSpawnPoints[index].position, Quaternion.identity) as GameObject;
            spawnedCannibals.Add(cannibal);
            index++;
        }

        cannibalCount = 0;
    }

    public void disableEnemy(GameObject enemy)
    {
        if (spawnedCannibals.Contains(enemy))
        {
            spawnedCannibals.Remove(enemy);
        }
        if (spawnedBoars.Contains(enemy))
        {
            spawnedBoars.Remove(enemy);
        }
    }

    public void enemyDied(bool isCannibal)
    {
        if (isCannibal)
        {
            cannibalCount++;
            if (cannibalCount > initialCannibalCount)
            {
                cannibalCount = initialCannibalCount;
            }
        }
        else
        {
            boarCount++;
            if (boarCount > initialBoarCount)
            {
                boarCount = initialBoarCount;
            }
        }
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(enemySpawnTimer);

        spawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void stopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

    public void disableEnemyControllers()
    {

    }
}
