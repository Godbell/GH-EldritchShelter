using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {

	[Serializable]

	public enum TileType {
		Wall, Floor, Door, Enemy,
	}

	// public IntRange 변수명 = new IntRange(min,max);
	// 
	public int mapWidth = 100; 								//맵 너비
	public int mapHeight = 100;                             //맵 높이

	public GameObject[] floorTiles;							//바닥 타일 배열	: Scene당 3개
	public GameObject[] wallTiles;				
	public GameObject doorTile;
	public GameObject player;
	public GameObject[] enemies;
    public GameObject[] guns;
    public GameObject capsule;

	private TileType[][] tiles;								//그리드!
	private Rooms[] rooms;									//방배열
	private Corridors[] corridors;								//통로배열
	private IntRange length = new IntRange(5,10);	
	//private GameObject boardHolder;							//맵홀더 - Hierarchy 관리

	public IntRange roomNum = new IntRange(8,15);			//방 개수
	public IntRange roomHeight = new IntRange(3,8);			//방 높이 x`
	public IntRange roomWidth = new IntRange(3,8);          //방 넓이
    public IntRange avaiL = new IntRange(0, 5);             //percentage
    public IntRange avaiLII = new IntRange(0, 600);
    public IntRange oneOrTwo = new IntRange(1, 2);

    public void SetupScene()
    {
		
			SetupTilesArray ();

			CreateRoomsAndCorridors ();

			SetTilesValueForRooms ();
			SetTilesValueForCorridors ();

			SetTilesValueForEnemies ();

			LetsInstantiate_Tiles ();

            /*System.Random r = new System.Random();
            int enemyCount = (int)r.Next(8, 12);*/
            //System라인의 Random함수로 8~12까지의 수를 랜덤으로 정함
        

			//SpawnEnemies (enemies, 8, 15);

	}


	public void SetupTilesArray() {
	
		if (tiles == null) {
			
			tiles = new TileType[mapWidth][];

			for (int i = 0; i < tiles.Length; i++) {
		
				tiles[i] = new TileType[mapHeight];

			}
		}
	}

	void CreateRoomsAndCorridors () {

			rooms = new Rooms[roomNum.Random];
			corridors = new Corridors[rooms.Length - 1];	

			//first room
			rooms [0] = new Rooms ();
			corridors [0] = new Corridors ();

			rooms [0].SetupRooms (roomWidth, roomHeight, mapWidth, mapHeight);
			corridors [0].SetupCorridors (rooms [0], length, roomWidth, roomHeight, mapWidth, mapHeight, true); //주의

			for (int i = 1; i < rooms.Length; i++) {
		
				rooms [i] = new Rooms ();

			rooms [i].SetupRooms (roomWidth, roomHeight, mapWidth, mapHeight,corridors[i - 1]);
				

				if (i < corridors.Length) {
					corridors [i] = new Corridors ();
					corridors [i].SetupCorridors (rooms [i], length, roomWidth, roomHeight, mapWidth, mapHeight, false);
				}
		}
	}

	void SetTilesValueForEnemies() {

		for (int i = 0; i < corridors.Length; i++) {

			Corridors currentCorridors = corridors [i];

			int xCoord = currentCorridors.startX;

			int yCoord = currentCorridors.startY;

			tiles [xCoord] [yCoord] = TileType.Enemy;
		}

	for (int i = 0; i < corridors.Length; i++) {

			Corridors currentCorridors = corridors [i];

			int xCoord = currentCorridors.EndPosX;

			int yCoord = currentCorridors.EndPosY;

			tiles [xCoord] [yCoord] = TileType.Enemy;
		}

	
	}


	void SetTilesValueForRooms () {

		if (tiles != null) {
			
			for (int i = 0; i < rooms.Length; i++) {

				Rooms currentRoom = rooms [i];

				for (int j = 0; j < currentRoom.roomWidth; j++) {
			
					int xCoord = currentRoom.xPos + j;

					for (int k = 0; k < currentRoom.roomHeight; k++) {
				
						int yCoord = currentRoom.yPos + k;	

						tiles [xCoord] [yCoord] = TileType.Floor;
					}
			
				}
			}
		}

	}


	void SetTilesValueForCorridors () {
		
		for (int i = 0; i < corridors.Length; i++) {
			Corridors currentCorridor = corridors [i];

			for (int j = 0; j < currentCorridor.length; j++) {
			
				int xCoord = currentCorridor.startX;
				int yCoord = currentCorridor.startY;

				switch (currentCorridor.direction) {
				case Direction.North:
					yCoord += j;
					break;
				case Direction.South:
					yCoord -= j;
					break;
				case Direction.West:
					xCoord -= j;
					break;
				case Direction.East:
					xCoord += j;
					break;
				}

				tiles [xCoord] [yCoord] = TileType.Floor;
			}
		}
	}

	public void LetsInstantiate_Tiles() { //현재 버그뿜뿜하는 함수

		

			for (int i = 0; i < tiles.Length; i++) {
		
				for (int j = 0; j < tiles [i].Length; j++) {
					
				if (tiles [i] [j] == TileType.Floor || tiles [i] [j] == TileType.Enemy) {

					InstantiateFromArray (floorTiles, i, j);
                    int randV = avaiLII.Random;
                    if (randV < 400 && randV > 390)
                    {
                        int randomVal = UnityEngine.Random.Range(1,2);
                        
                            Vector3 medicPos = new Vector3(i, j, 0);
                            Instantiate(capsule, medicPos, Quaternion.identity);
                        
                        
                        
                    }
                    else if(randV < 600 && randV > 590) {

                        Vector3 gunPos = new Vector3(i, j, 0);
                        IntRange gunChoice = new IntRange(0, guns.Length - 1);
                        Instantiate(guns[gunChoice.Random], gunPos, Quaternion.identity);

                    }
				}
					if (tiles [i] [j] == TileType.Wall) {

						InstantiateFromArray (wallTiles, i, j);

				}
				if (tiles [i] [j] == TileType.Enemy && i != 50 && j != 50) {

					if (avaiL.Random == 3 || avaiL.Random == 1) {
                        IntRange tmp = new IntRange(0, enemies.Length - 1);
                        Vector3 pos = new Vector3(i, j, 0);
						Instantiate (enemies[tmp.Random],pos, Quaternion.identity);
					}
				}
			}
        }
        
		Vector3 doorPos = new Vector3 (rooms [rooms.Length - 1].xDoor, rooms [rooms.Length - 1].yDoor, 0);
		Instantiate (doorTile, doorPos, Quaternion.identity);

}

	void InstantiateFromArray(GameObject[] prefabs, float xC, float yC) {
		if (tiles != null) {
			int randomIndex = UnityEngine.Random.Range (0, prefabs.Length);
			Vector3 position = new Vector3 (xC, yC, 0f);
			Instantiate (prefabs [randomIndex], position, Quaternion.identity);

		}
	}

}
