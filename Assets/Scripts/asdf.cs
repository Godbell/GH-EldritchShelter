using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class asdf : MonoBehaviour {
    
    public void ToGame()
    {
        Player.passedexit = 0;
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
       
    }
}
