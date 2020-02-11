using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour {

	// Use this for initialization
	public void resume ()
    {
        GameManager.doingSetup = false;
        GameManager.PauseImage.SetActive(false);
	}

}
