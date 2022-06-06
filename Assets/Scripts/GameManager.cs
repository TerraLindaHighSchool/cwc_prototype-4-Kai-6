using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public bool gameActive = false;

    public GameObject player;
    public GameObject[] trees;

    public Button startButton;

    private GenerateTerrain terrainGenScript;

    private Terrain terrain;

    public float treesToPlace = 1;
    void Start()
    {
        terrainGenScript = GameObject.Find("Terrain").GetComponent<GenerateTerrain>();
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        startButton.onClick.AddListener(StartGame);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartGame()
    {
        terrainGenScript.GenerateHeights();
        placeTrees();
        placePlayer();
        gameActive = true;
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
        Physics.Raycast(new Vector3(placePos.x, 500, placePos.z), Vector3.down, out hit, 2000);
        placePos.y = hit.point.y + 1.2f;
        player.transform.position = placePos;
    }
}
