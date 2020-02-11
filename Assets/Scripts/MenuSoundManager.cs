using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public AudioSource MainMenuMusicSource;
    public AudioSource efxButtonSource;
    public static MenuSoundManager instance = null;
  
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        

    }

    public void PlayButtonSound(AudioClip clip)
    {
        efxButtonSource.clip = clip;
        efxButtonSource.Play();
    }
    public void PlayMenuMusic (AudioClip clip)
    {
        if (this)
        {
            MainMenuMusicSource.clip = clip;
            MainMenuMusicSource.Play();
        }
    }
  
}