using System.Collections;
using UnityEngine;

public class GameManager1 : MonoBehaviour {

	public BoardManager boardScript;
	public static GameManager1 instance = null ;

	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame() {

	boardScript.SetupTilesArray ();

	}

	void Start() {
	


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}