using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {

	public enum TileType {
		Wall, Floor, Door,
	}

	// public IntRange 변수명 = new IntRange(min,max);
	// 
	public int mapWidth = 100; 								//맵 너비
	public int mapHeight = 100;								//맵 높이
		

	public GameObject[] floorTiles;							//바닥 타일 배열	: Scene당 3개
	public GameObject[] wallTiles;				
	public GameObject[] doorTile;
	public GameObject player;

	private TileType[][] tiles;								//그리드!
	private Rooms[] rooms;									//방배열
	private Corridors[] corridors;								//통로배열
	private IntRange length = new IntRange(5,10);	
	//private GameObject boardHolder;							//맵홀더 - Hierarchy 관리

	public IntRange roomNum = new IntRange(8,15);			//방 개수
	public IntRange roomHeight = new IntRange(3,8);			//방 높이 x`
	public IntRange roomWidth = new IntRange(3,8);			//방 넓이

	public GameObject fogTile;

	/// <summary>
	/// Start this instance.
	/// </summary>
	public void SetupDoors() {
	

	
	}

	///
	///
	///

	//
	// Use this for initialization
	void Start () {


			SetupTilesArray ();

			CreateRoomsAndCorridors ();

			SetTilesValueForRooms ();
			SetTilesValueForCorridors ();
		SetTilesValueForDoors ();

			LetsInstantiate_Tiles ();

		/*Vector3 position = new Vector3(mapHeight / 2f, mapHeight / 2f, 0f);
		Instantiate (player, position, Quaternion.identity);
*/
	//	AllInvisible ();
	//	DrawCircle ();
	//	ProceedDot ();

	}

	void Update() {
	

	
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

	void SetTilesValueForDoors() {

		for (int i = 0; i < corridors.Length; i++) {

			Corridors currentCorridors = corridors [i];

			int xCoord = currentCorridors.startX;

			int yCoord = currentCorridors.startY;

			tiles [xCoord] [yCoord] = TileType.Door;
		}

	for (int i = 0; i < corridors.Length; i++) {

			Corridors currentCorridors = corridors [i];

			int xCoord = currentCorridors.EndPosX;

			int yCoord = currentCorridors.EndPosY;

			tiles [xCoord] [yCoord] = TileType.Door;
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
					
				if (tiles [i] [j] == TileType.Floor) {

					InstantiateFromArray (floorTiles, i, j);

				}
					if (tiles [i] [j] == TileType.Wall) {

						InstantiateFromArray (wallTiles, i, j);

				}
			if (tiles [i] [j] == TileType.Door) {
					
				InstantiateFromArray (doorTile, i, j);
				}
			
			//	InstantiateFromArray (fogTile, i, j);

			}
		}
}

	void InstantiateFromArray(GameObject[] prefabs, float xC, float yC) {
		if (tiles != null) {
			int randomIndex = UnityEngine.Random.Range (0, prefabs.Length);
			Vector3 position = new Vector3 (xC, yC, 0f);
			Instantiate (prefabs [randomIndex], position, Quaternion.identity);

		}
	}


/* fffffffffffffffffffffffffoooooooooooooooooooooooggggggggggggggggggggggggg ooooooooooooooooooooooooffffffffffffffffffffffff      wwwwwwwwwwwwwwwwwwwwwwwwaaaaaaaaaaaaaaaaaaaaaaaaaaaaarrrrrrrrrrrrrrrrrrrrrrr
zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz
zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz */

public void AllInvisible() {
		
		for (int i = 0; i < tiles.Length; i++) {

			for (int j = 0; j < tiles [i].Length; j++) {

				Vector3 position = new Vector3 (i, j, 0f);
				Instantiate (fogTile, position, Quaternion.identity);

			}
	
		}
			
}

/*public void DrawCircle() {



}

public void ProceedDot() {

}

}*/







}
