﻿using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class Loader1 : MonoBehaviour {

	public GameManager1 gameManager;

	void Awake() {
	
		if (GameManager1.instance == null) {
			Instantiate (gameManager);
		}

	}
}
