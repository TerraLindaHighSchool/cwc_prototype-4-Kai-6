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

    public QualityMode quality;
    
    public Terrain terrain;

    private void Start()
    {
    }
    public void GenerateHeights()
    {
        ModuleBase moduleBase;

        Billow billow = new Billow();
        billow.Seed = seed;
        billow.Frequency = frequency;
        billow.Lacunarity = lunacarity;
        billow.OctaveCount = octaveCount;
        billow.Persistence = persistance;
        billow.Quality = quality;

        Perlin perlin = new Perlin();
        perlin.Seed = seed+1;
        perlin.Frequency = frequency;
        perlin.Lacunarity = lunacarity;
        perlin.OctaveCount = octaveCount;
        perlin.Persistence = persistance;
        perlin.Quality = quality;

        RidgedMultifractal ridged = new RidgedMultifractal();
        ridged.Seed = seed+2;
        ridged.Frequency = frequency;
        ridged.Lacunarity = lunacarity;
        ridged.OctaveCount = octaveCount;
        ridged.Gain = gain;
        ridged.Quality = quality;

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
        Noise2D generator = new Noise2D(terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution, moduleBase);
        generator.GeneratePlanar(-1, 1, -1, 1, true);
        float[,] heights = generator.GetNormalizedData();
        Debug.Log(heights[100,100]);
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}