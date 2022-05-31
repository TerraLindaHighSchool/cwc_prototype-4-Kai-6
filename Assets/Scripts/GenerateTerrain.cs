using UnityEditor;
using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour
{

    public float HillFrequency = 10.0f;
    public float LowestHillHeight;
    public float HighestHillHeight;
    public Terrain terrain;

    private void Start()
    {
        GenerateHeights(terrain, HillFrequency);
    }
    public void GenerateHeights(Terrain terrain, float tileSize)
    {
        float hillHeight = (float)((float)HighestHillHeight - (float)LowestHillHeight) / ((float)terrain.terrainData.heightmapResolution / 2);
        float baseHeight = (float)LowestHillHeight / ((float)terrain.terrainData.heightmapResolution / 2);
        float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

        for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
            {
                heights[i, k] = baseHeight + (Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapResolution) * tileSize, ((float)k / (float)terrain.terrainData.heightmapResolution) * tileSize) * (float)hillHeight);
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}