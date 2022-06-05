using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private GameManager gameManager;
    public TextMeshProUGUI waveCounter;

    private bool isStartingNextWave = false;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameActive) return;
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
        
    }
}
