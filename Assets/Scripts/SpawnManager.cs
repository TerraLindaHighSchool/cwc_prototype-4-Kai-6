using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameManager gameManager;
    public float minSpawnDelayPeace = 10f;
    public float maxSpawnDelayPeace = 15f;
    public float minSpawnDelay = 6f;
    public float maxSpawnDelay = 10f;
    public float maxGroupSize = 4;
    private float groupSize = 3;
    private float delay;
    public float enemyHealth = 50;
    public float difficultyTick = 240;

    public bool peace = false;

    private float spawnRange = 500;
    void Start()
    {
        delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
    }

    public IEnumerator spawnEnemies()
    {
        while (true)
        {
            if (!peace)
            {
                yield return new WaitForSeconds(delay);
                groupSize = Mathf.RoundToInt(Random.Range(maxGroupSize - 2, maxGroupSize));
                for (int i = 0; i < groupSize; i++)
                {
                    Vector3 randomPos = GenerateSpawnPosition();
                    Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
                }
                delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            } else
            {
                yield return new WaitForSeconds(delay);
                groupSize = 1;
                Vector3 randomPos = GenerateSpawnPosition();
                Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
                delay = Random.Range(minSpawnDelayPeace, maxSpawnDelayPeace);
            }
        }
    }

    public IEnumerator modeSwitch()
    {
        while (true)
        {
            if (!peace) {
                yield return new WaitForSeconds(Random.Range(3 * 60, 5 * 60));
                peace = true;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(60, 2 * 60));
                peace = false;
            }
        }
    }

    public IEnumerator difficultyOverTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(difficultyTick);

            if (Random.Range(0, 100) < 50)
            {
                if (maxGroupSize < 15)
                {
                    maxGroupSize++;
                }
                else
                {
                    enemyHealth += 5;
                }
            } else {
                enemyHealth += 10;
            }
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 500, spawnPosZ);
        RaycastHit hit;
        Physics.Raycast(randomPos, Vector3.down, out hit, 1000);
        randomPos.y = hit.point.y;
        return randomPos;
    }
}
