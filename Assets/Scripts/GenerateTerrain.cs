using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class GenerateTerrain : MonoBehaviour
{
    public enum NoiseType{
        Billow,
        Perlin,
        Ridged,
        BillowPerlin,
        BillowRidge,
        PerlinRidge
    }
    public int seed;
    public float frequency = 10.0f;
    public float lunacarity;
    public float persistance;
    public int octaveCount;
    public float scaleFactor;
    public float bias;
    public float specWeightEx;
    public float gain;
    public NoiseType noiseType;
    private int resolution;

    private float[,] gradArray;

    public QualityMode quality;
    
    public Terrain terrain;

    private void Start()
    {
        resolution = terrain.terrainData.heightmapResolution;
        seed = Random.Range(0, 10000);
    }
    public void GenerateHeights()
    {
        
        ModuleBase moduleBase;

        Billow billow = new Billow
        {
            Seed = seed,
            Frequency = frequency,
            Lacunarity = lunacarity,
            OctaveCount = octaveCount,
            Persistence = persistance,
            Quality = quality
        };

        Perlin perlin = new Perlin
        {
            Seed = seed + 1,
            Frequency = frequency,
            Lacunarity = lunacarity,
            OctaveCount = octaveCount,
            Persistence = persistance,
            Quality = quality
        };

        RidgedMultifractal ridged = new RidgedMultifractal
        {
            Seed = seed + 2,
            Frequency = frequency,
            Lacunarity = lunacarity,
            OctaveCount = octaveCount,
            Gain = gain,
            Quality = quality
        };

        switch (noiseType)
        {
            case NoiseType.Billow:
                moduleBase = billow;
                break;
            case NoiseType.Perlin:
                moduleBase = perlin;
                break;
            case NoiseType.Ridged:
                moduleBase = ridged;
                break;
            case NoiseType.BillowPerlin:
                Add billPerl = new Add(billow, perlin);
                moduleBase = billPerl;
                break;
            case NoiseType.BillowRidge:
                Add billRidge = new Add(billow, ridged);
                moduleBase = billRidge;
                break;
            case NoiseType.PerlinRidge:
                Add perlRidge = new Add(perlin, ridged);
                moduleBase = perlRidge;
                break;
            default:
                moduleBase = perlin;
                break;
        }
        moduleBase = new ScaleBias(scaleFactor, bias, moduleBase);
        Noise2D generator = new Noise2D(resolution, resolution, moduleBase);
        generator.GeneratePlanar(-1, 1, -1, 1, true);
        float[,] heights = generator.GetNormalizedData();
        terrain.terrainData.SetHeights(0, 0, heights);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
    float map(float value, float currentMin, float currentMax, float newMin, float newMax)
    {
        return newMin + (value - currentMin) * (newMax - newMin) / (currentMax - currentMin);
    }
}