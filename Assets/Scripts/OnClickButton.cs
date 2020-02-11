using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickButton : MonoBehaviour {




    public static OnClickButton instance = null;


    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);



    }
    public void playClip()
    {
        Player.passedexit = 0;
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }
}
