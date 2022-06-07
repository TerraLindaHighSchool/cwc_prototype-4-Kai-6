using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using UnityEngine.AI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool gameActive = false;

    public GameObject player;
    public GameObject[] trees;
    public GameObject titleScreen;
    public GameObject gameOverScreen;

    public Button startButton;
    public Button retryButton;
    public Dropdown difficultyDropdown;

    private GenerateTerrain terrainGenScript;
    private SpawnManager spawnManager;
    private MusicController musicController;
    public GameObject healthBar;
    public TextMeshProUGUI healthText;

    private Terrain terrain;

    public float treesToPlace = 1;
    void Start()
    {
        
        terrainGenScript = GameObject.Find("Terrain").GetComponent<GenerateTerrain>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        musicController = GameObject.Find("MusicController").GetComponent<MusicController>();
        titleScreen = GameObject.Find("TitleScreen");
        startButton.onClick.AddListener(StartGame);
        retryButton.onClick.AddListener(Retry);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive) return;
        healthBar.GetComponent<RectTransform>().SetRight((player.GetComponent<PlayerController>().maxHealth - player.GetComponent<PlayerController>().health) * 5);
        healthText.text = "HP: " + player.GetComponent<PlayerController>().health + " / " + player.GetComponent<PlayerController>().maxHealth;


    }


    void StartGame()
    {
        terrainGenScript.GenerateHeights();
        placeTrees();
        placePlayer();
        gameActive = true;
        spawnManager.enemyHealth *= difficultyDropdown.value+1;
        spawnManager.StartCoroutine(spawnManager.spawnEnemies());
        spawnManager.StartCoroutine(spawnManager.difficultyOverTime());
        spawnManager.StartCoroutine(spawnManager.modeSwitch());
        musicController.currentlyPlaying.Play();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Retry()
    {
        terrainGenScript.GenerateHeights();
        placeTrees();
        placePlayer();
        gameActive = true;
        spawnManager.enemyHealth *= difficultyDropdown.value + 1;
        spawnManager.StartCoroutine(spawnManager.spawnEnemies());
        spawnManager.StartCoroutine(spawnManager.difficultyOverTime());
        spawnManager.StartCoroutine(spawnManager.modeSwitch());
        gameOverScreen.SetActive(false);
        musicController.currentlyPlaying.Play();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void placeTrees()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 placePos = new Vector3(Random.Range(-500+(i*100), -500+(i*100)+100), 0, Random.Range(-500 + (j * 100), -500 + (j * 100) + 100));
                RaycastHit hit;
                Physics.Raycast(new Vector3(placePos.x, 500, placePos.z), Vector3.down, out hit, 2000);
                placePos.y = hit.point.y;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                int index = Mathf.FloorToInt(Random.Range(0, trees.Length));
                Instantiate(trees[index], placePos, rotation);
            }
        }
        
                
    }

    void placePlayer()
    {
        Vector3 placePos = new Vector3(Random.Range(-400, 400), 0, Random.Range(-400, 400));
        RaycastHit hit;
        Physics.Raycast(new Vector3(placePos.x, 300, placePos.z), Vector3.down, out hit, 2000);
        
        placePos.y = hit.point.y + 1.2f;
        player.transform.position = placePos;
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void GameOver()
    {
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(true);
        gameActive = false;
        Cursor.lockState = CursorLockMode.None;
        spawnManager.StopCoroutine(spawnManager.spawnEnemies());
        spawnManager.StopCoroutine(spawnManager.difficultyOverTime());
        spawnManager.StopCoroutine(spawnManager.modeSwitch());
    }
}
