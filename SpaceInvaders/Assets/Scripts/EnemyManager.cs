using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Layout")] public int numEnemiesAcross = 6;
    public float widthPerEnemy = 2f;
    public float heightPerEnemy = 2f;

    [Header("Gameplay Settings")] 
    public float secondsPerStep = 0.5f;

    [Range(0.1f, 2)] public float minSheetInterval = 1f;
    [Range(2f, 10f)] public float maxShootInterval = 7f;

    [Header("Enemy Prefabs")] 
    public Transform enemy1Prefab;
    public Transform enemy2Prefab;
    public Transform enemy3Prefab;

    [Header("References")] 
    public Transform enemyRoot;
    private int totalNumberOfEnemies;
    
    private int enemyAlive;
    private Vector3 marchDirection = Vector3.right;
    private float currentShotInterval;
    private float timeSinceLastStep;
    private float timeSinceLastShot;

    
    private float enemyAliveinPercent;
    

    
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    private void Start()
    {
        float windowHeight = Camera.main.orthographicSize * 2;
        float enemyStartHeight = windowHeight - heightPerEnemy * 2.5f;
        enemyStartHeight = enemyStartHeight - 4.5f;
        SpawnEnemyRow(enemy1Prefab, enemyStartHeight);
        SpawnEnemyRow(enemy2Prefab, enemyStartHeight - heightPerEnemy);
        SpawnEnemyRow(enemy3Prefab, enemyStartHeight - heightPerEnemy * 2f);
        currentShotInterval = Random.Range(minSheetInterval, maxShootInterval);

        foreach (Transform enemyTransform in enemyRoot)
        {
            totalNumberOfEnemies += 1;
        }
        
        // https://www.youtube.com/watch?v=qWDQgmdUzWI
        InvokeRepeating(nameof(enemyAttack), currentShotInterval, currentShotInterval);
    }

    private void Update()
    {
        enemyAlive = 0;
        
        foreach (Transform enemyTransform in enemyRoot)
        {
            enemyAlive = enemyAlive + 1;
        }
        
        timeSinceLastStep += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastStep > secondsPerStep)
        {
            timeSinceLastStep -= secondsPerStep;
            enemyRoot.position += marchDirection * widthPerEnemy * 0.5f;

            float horizontalExtent = Camera.main.orthographicSize * Camera.main.aspect - widthPerEnemy;
            foreach (Transform enemyTransform in enemyRoot)
            {
                if (Mathf.Abs(enemyTransform.position.x) > horizontalExtent)
                {
                    enemyRoot.position += Vector3.down * heightPerEnemy;
                    marchDirection *= -1f;
                    break;
                }
            }
        }
        enemyAliveinPercent = (float) enemyAlive / totalNumberOfEnemies;
        if (timeSinceLastShot > currentShotInterval)
        {
            timeSinceLastShot -= currentShotInterval;
            currentShotInterval = Random.Range(minSheetInterval, maxShootInterval);
            
        }
        

    }

    void SpawnEnemyRow(Transform enemyPrefab, float height)
    {
        for (int i = 0; i < numEnemiesAcross; i++)
        {
            float xPos = -(numEnemiesAcross * widthPerEnemy) / 2 + i * widthPerEnemy + widthPerEnemy / 2;
            Transform enemy = Instantiate(enemyPrefab, new Vector3(xPos, height, 0f), Quaternion.identity);
            enemy.SetParent(enemyRoot);
        }
    }

    

    void enemyAttack()
    {
        // https://www.youtube.com/watch?v=qWDQgmdUzWI
        foreach (Transform enemyTransform in enemyRoot)
        {
            if (Random.value < (1.0f / (float) enemyAliveinPercent))
            {
                GameObject shot = Instantiate(bulletPrefab, enemyTransform.position, Quaternion.identity);
                shot.tag = "enemy";
                break;
            }
        }
    }
    
    
}
