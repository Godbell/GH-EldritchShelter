using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitesc : MonoBehaviour {
    public AudioClip MenuMusic;
    public GameObject soundManager;

    void Awake()
    {
        Instantiate(soundManager);
        MenuSoundManager.instance.PlayMenuMusic(MenuMusic);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.google.com");
#else
        Application.Quit();
#endif
        }
    }
}
