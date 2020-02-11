using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static InGameSoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    
    }

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();

    }
    public void RandomizeInGameMusic(params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        musicSource.clip = clips[randomIndex];
        musicSource.Play();
    }
}
