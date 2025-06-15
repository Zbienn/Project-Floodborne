using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float spawnCooldown;
    private float SpawnCounter;

    [SerializeField] private Transform minSpawn, maxSpawn;

    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new();
    [SerializeField] private int checksPerFrame;
    private int enemyToCheck;

    [SerializeField] private List<WaveInfo> waves;
    private int currentWave;
    private float waveCounter;

    void Start()
    {
        //SpawnCounter = spawnCooldown;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 3f;

        currentWave = -1;
        GoToNextWave();


    }

    void Update()
    {
        /* SpawnCounter -= Time.deltaTime;
         if (SpawnCounter <= 0)
         {
             SpawnCounter = spawnCooldown;
             GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
             spawnedEnemies.Add(newEnemy);
         }*/

        if (gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GoToNextWave();
                }

                SpawnCounter -= Time.deltaTime;
                if (SpawnCounter <= 0)
                {
                    SpawnCounter = waves[currentWave].TimeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].EnemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        int checkTarget = enemyToCheck + checksPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    private Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            } else {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }


    public void GoToNextWave()
    {
        currentWave++;

        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].WaveLength;
        SpawnCounter = waves[currentWave].TimeBetweenSpawns;
    }
}

[System.Serializable]
public class WaveInfo
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float waveLength = 10f;
    [SerializeField] private float timeBetweenSpawns = 1f;

    public GameObject EnemyToSpawn { get => enemyToSpawn; set => enemyToSpawn = value; }
    public float WaveLength { get => waveLength; set => waveLength = value; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
}
