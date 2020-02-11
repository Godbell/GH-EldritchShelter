using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerHP = 100;
    public static bool doingSetup;
    public static GameObject PauseImage;
    public static bool enemiesMoving;
    public AudioClip Stage1;
    public AudioClip Stage2;
	public AudioClip Stage3;

    [HideInInspector] public bool playersTurn = true;
    public GameObject soundManager;
    public GameObject menuSoundManager;
    private Text levelText;
    private GameObject levelImage;
    private GameObject DieImage;
    private Text DieText;
    private int level = 1;
    private List<Enemy> enemies;
	private Player playerScript;

    public AudioClip buttonSound;

    public Vector3 def = new Vector3(0, 0, 0);


    // Use this for initialization
    void Awake()
    {

        if (instance == null)
            instance = this;
            
        
        else if (instance != this)
            Destroy(gameObject);
        Instantiate(soundManager);
        InGameSoundManager.instance.PlaySingle(buttonSound);
        InGameSoundManager.instance.RandomizeInGameMusic(Stage1, Stage2, Stage3);


        if (Player.passedexit == 1)
        {
            DontDestroyOnLoad(gameObject);
            level++;
        }
        else if (Player.passedexit == 0)
        {
            enabled = true;
        }
        playerHP = Player.newfood;
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        DieText = GameObject.Find("DieText").GetComponent<Text>();
        DieImage = GameObject.Find("DieImage");
        PauseImage = GameObject.Find("PauseImage");


        PauseImage.SetActive(false);
        DieImage.SetActive(false);
        levelText.text = "스테이지 " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene();
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {

        DieText.text = "스테이지 " + level + " 에서 사망하셨습니다.";
        DieImage.SetActive(true);
        enabled = false;
     
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doingSetup = true;
            PauseImage.SetActive(true);
        }
        if (playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }


    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);    
    }

	public void PlzDieEnemy(IntRange dmg, Collider2D other)
	{
		for (int i = 0; i < enemies.Count; i++) {

			if (other.tag == "Bullet") {

				enemies [i].enemyHP -= playerScript.playerDNG.Random;
				playerScript.DeleteHit ();
				enemies [i].CheckDie ();
			}
		}
	}

    void Die()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies = null)
        {
            GetComponent<Enemy>().enabled = false;
        }

    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }


}

