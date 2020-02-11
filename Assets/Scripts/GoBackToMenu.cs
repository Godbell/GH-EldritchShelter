using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMenu : MonoBehaviour
{
    public GameObject soundManager;
    public AudioClip MenuMusic;


    public void GoBackToMenub()
    {

        Player.passedexit = 0;
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        Instantiate(soundManager);
        MenuSoundManager.instance.PlayMenuMusic(MenuMusic);
    }
}