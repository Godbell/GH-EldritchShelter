using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MovingObject

	{
	public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.
	public int pointsPerCapsule = 10;				//Number of points to add to player food points when picking up a food obj
	public int wallDamage = 1;					//How much damage a player does to a wall when chopping it.
	public Text HPText;
	public IntRange playerDNG = new IntRange(1,3);
	public Enemy enemyScript;

    public static int newfood = 100; //UI Text to display current player food total.
    public AudioClip Exit;
    public AudioClip PickUpGun;
    public AudioClip Fire;
    public AudioClip Capsule;
    public AudioClip gameOver;
    public AudioClip playerHit;
    public AudioClip gameOverBGM;
    public GameObject hitBox;
    private GameObject currentItem;


    private Animator animator;					//Used to store a reference to the Player's animator component.
		private int food;            
	//Used to store player food points total during level.
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
#endif
		
		
		//Start overrides the Start function of MovingObject
		protected override void Start ()
		{
			//Get a component reference to the Player's animator component
			animator = GetComponent<Animator>();
			
			//Get the current food point total stored in GameManager.instance between levels.
			food = GameManager.instance.playerHP;
			
			//Set the foodText to reflect the current player food total.
		
			HPText.text = "현재 체력 : " + food; 
			
			//Call the Start function of the MovingObject base class.
			base.Start ();
		}
		
		
		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
        //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
			GameManager.instance.playerHP = food;
		}
		
		
		private void Update ()
		{
			//If it's not the player's turn, exit the function.
			if(!GameManager.instance.playersTurn) return;
			
			int horizontal = 0;  	//Used to store the horizontal move direction.
			int vertical = 0;       //Used to store the vertical move direction.

        if (currentItem != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
				float mouseX = Input.mousePosition.x;
				float mouseY = Input.mousePosition.y;
				Vector3 mousePos = new Vector3 (mouseX, mouseY, 0);
				Instantiate (hitBox, mousePos, Quaternion.identity);
                GameManager.instance.playersTurn = false;
            }
        }
        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER
			
			//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
			horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
			
			//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
			vertical = (int) (Input.GetAxisRaw ("Vertical"));
			
			//Check if moving horizontally, if so set vertical to zero.
			if(horizontal != 0)
			{
				vertical = 0;
			}
			//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			
			//Check if Input has registered more than zero touches
			if (Input.touchCount > 0)
			{
				//Store the first tou	ch detected.
				Touch myTouch = Input.touches[0];
				
				//Check if the phase of that touch equals Began
				if (myTouch.phase == TouchPhase.Began)
				{
					//If so, set touchOrigin to the position of that touch
					touchOrigin = myTouch.position;
				}
				
				//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
				else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
				{
					//Set touchEnd to equal the position of this touch
					Vector2 touchEnd = myTouch.position;
					
					//Calculate the difference between the beginning and end of the touch on the x axis.
					float x = touchEnd.x - touchOrigin.x;
					
					//Calculate the difference between the beginning and end of the touch on the y axis.
					float y = touchEnd.y - touchOrigin.y;
					
					//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
					touchOrigin.x = -1;
					
					//Check if the difference along the x axis is greater than the difference along the y axis.
					if (Mathf.Abs(x) > Mathf.Abs(y))
						//If x is greater than zero, set horizontal to 1, otherwise set it to -1
						horizontal = x > 0 ? 1 : -1;
					else
						//If y is greater than zero, set horizontal to 1, otherwise set it to -1
						vertical = y > 0 ? 1 : -1;
				}
			}
			
#endif //End of mobile platform dependendent compilation section started above with #elif
        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
			{
				//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
				//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
				AttemptMove<Wall> (horizontal, vertical);

            int left;
            left = (int)Input.GetAxisRaw("Horizontal");

            if (left == -1)
                animator.SetTrigger("PlayerLeft");
            else if (left == 1)
                animator.SetTrigger("PlayerRight");
        }
		}
		
		//AttemptMove overrides the AttemptMove function in the base class MovingObject
		//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
		protected override void AttemptMove <T> (int xDir, int yDir)
		{

			HPText.text = "현재 체력 : " + food;

			base.AttemptMove <T> (xDir, yDir);

			RaycastHit2D hit;
			
			if (Move (xDir, yDir, out hit)) 
			{
				//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
				//SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
			}
			CheckIfGameOver ();
			
			GameManager.instance.playersTurn = false;
		}
		
		
		//OnCantMove overrides the abstract function OnCantMove in MovingObject.
		//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
		protected override void OnCantMove <T> (T component)
		{
			//Set hitWall to equal the component passed in as a parameter.
			Wall hitWall = component as Wall;
			
			//Call the DamageWall function of the Wall we are hitting.
			hitWall.DamageWall (wallDamage);
			
		}
		
		
		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Exit")
			{
            InGameSoundManager.instance.PlaySingle(Exit);
            
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", restartLevelDelay);
				
				//Disable the player object since level is over.
				enabled = false;
			}

        else if (other.tag == "Pistol") // 충돌된 오브젝트가 아이템이라면...
        {
			IntRange rand = new IntRange (0,5);

			switch(rand.Random){

			case 0: 
				playerDNG = new IntRange (1, 4);
				break;
			case 1: 
				playerDNG = new IntRange (1, 4);
				break;
			case 2: 
				playerDNG = new IntRange (1, 4);
				break;
			case 3:
				playerDNG = new IntRange (3, 6);
				break;
			case 4:
				playerDNG = new IntRange (3, 6);
				break;
			case 5:
				playerDNG = new IntRange (7, 7);
				break;
			
			}

			if (currentItem == null) {
				// 아이템을 플레이어의 Child로 지정해 줍니다.
				InGameSoundManager.instance.PlaySingle (PickUpGun);
				other.gameObject.transform.position = transform.position;
				other.gameObject.transform.localScale = new Vector2 (2f, 2f);
				other.gameObject.transform.parent = transform;
				other.GetComponent<Collider2D> ().enabled = false;
				currentItem = other.gameObject.gameObject;		
			}
        }

        //Check if the tag of the trigger collided with is Food.
        else if(other.tag == "Food")
			{
				//Add pointsPerFood to the players current food total.
				food += pointsPerCapsule;
                HPText.text = " + " + pointsPerCapsule + " / 현재 체력 : " + food;

            //Update foodText to represent current total and notify player that they gained points
            //		foodText.text = "+" + pointsPerFood + " Food: " + food;

            //Call the RandomizeSfx function of SoundManager and pass in two eating sounds to choose between to play the eating sound effect.
            InGameSoundManager.instance.PlaySingle(Capsule);

            //Disable the food object the player collided with.
            other.gameObject.SetActive (false);
			}
	/*		
			//Check if the tag of the trigger collided with is Soda.
			else if(other.tag == "Soda")
			{
				//Add pointsPerSoda to players food points total
				food += pointsPerSoda;
				
				//Update foodText to represent current total and notify player that they gained points
				//foodText.text = "+" + pointsPerSoda + " Food: " + food;
				
				//Call the RandomizeSfx function of SoundManager and pass in two drinking sounds to choose between to play the drinking sound effect.
			//	SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound2);
				
				//Disable the soda object the player collided with.
				other.gameObject.SetActive (false);
			}*/
		}

    public static int passedexit;

		//Restart reloads the scene when called.
		private void Restart ()
		{
        //Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
        //and not load all the scene object in the current scene.
        passedexit = 1;
        newfood = food;
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
        
        }



    //LoseFood is called when an enemy attacks the player.
    //It takes a parameter loss which specifies how many points to lose.
    public void LoseHP(int loss)
    {
        //Set the trigger for the player animator to transition to the playerHit animation.

        //Subtract lost food points from the players total.
        food -= loss;
        HPText.text = "- " + loss + " / 현재 체력 : " + food;

        //Update the food display with the new total.
        //		foodText.text = "-"+ loss + " Food: " + food;

        {
            InGameSoundManager.instance.PlaySingle(playerHit);
        }
            //Check to see if game has ended.
            
        
        CheckIfGameOver();
    }
		
	public void DeleteHit() {
	
		Destroy (hitBox);
	
	}


		//CheckIfGameOver checks if the player is out of food points and if so, ends the game.
		private void CheckIfGameOver ()
		{
			//Check if food point total is less than or equal to zero.
			if (food <= 0) 
			{
				//Call the PlaySingle function of SoundManager and pass it the gameOverSound as the audio clip to play.
				
				//Stop the background music.
				InGameSoundManager.instance.musicSource.Stop();
                InGameSoundManager.instance.PlaySingle(gameOver);
                InGameSoundManager.instance.RandomizeInGameMusic(gameOverBGM);
                //Call the GameOver function of GameManager.
                GameManager.instance.GameOver ();
			}
		}
	}


