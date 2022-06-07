using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public SpawnManager spawnManager;

    public AudioClip nightBattle;
    public AudioClip dayBattle;
    public AudioClip nightPeace;
    public AudioClip dayPeace;
    private AudioClip newMusic;
    public AudioSource currentlyPlaying;

    private float fadeDuration = 3f;
    private string currentState = "Peace";
    // Start is called before the first frame update
    void Start()
    {
        currentlyPlaying.clip = dayBattle;
    }

    public IEnumerator transition()
    {
        while (currentlyPlaying.volume > 0)
            {
                currentlyPlaying.volume -= (1 / fadeDuration) * Time.deltaTime;
                yield return null;
            }
        if (currentlyPlaying.volume == 0)
        {
            currentlyPlaying.Stop();
            currentlyPlaying.clip = newMusic;
            currentlyPlaying.Play();
            currentlyPlaying.volume = 1;
            StopCoroutine("transition");
        }
    }
    void Update()
    {
        string newState = "";
        if (spawnManager.peace)
        {
            newState = "Peace";
            newMusic = dayPeace;
        }
        else
        {
            newState = "Battle";
            newMusic = dayBattle;
        }

        if (currentState != newState)
        {
            StartCoroutine(transition());
            currentState = newState;
        }
    }
}
