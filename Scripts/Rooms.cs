using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms {

	public int xPos;						//시작 x좌표
	public int yPos;						//시작 y좌표
	public int roomWidth;					//방너비
	public int roomHeight;					//방높이
	public Direction enteringCorridor;		//들어오는 통로

	public void SetupRooms (IntRange widthRange, IntRange heightRange, int mapWidth, int mapHeight) {

		roomWidth = widthRange.Random;
		roomHeight = heightRange.Random;

		xPos = Mathf.RoundToInt (mapWidth / 2f - roomWidth / 2f);
		yPos = Mathf.RoundToInt (mapHeight / 2f - roomHeight / 2f);

	}

	public void SetupRooms (IntRange widthRange, IntRange heightRange, int mmapWidth, int mmapHeight, Corridors corridorss) {

		enteringCorridor = corridorss.direction;

		roomWidth = widthRange.Random;
		roomHeight = widthRange.Random;

		switch (corridorss.direction) {

		/*case Direction.North:
			roomHeight = Mathf.Clamp (roomHeight, 1, mmapHeight - corridorss.EndPosY);

			yPos = corridorss.EndPosY;
			xPos = Random.Range (corridorss.EndPosX - roomWidth + 1, corridorss.EndPosX);

			xPos = Mathf.Clamp (xPos, 0, mmapWidth - roomWidth);
			break;

		case Direction.West:
			roomWidth = Mathf.Clamp (roomWidth, 1, roomWidth - corridorss.EndPosX);

			yPos = Random.Range (corridorss.EndPosY - roomHeight + 1, corridorss.EndPosY);
			xPos = corridorss.EndPosX - roomWidth;
						
			yPos = Mathf.Clamp (yPos, 0, mmapHeight - roomHeight);
			break;

		case Direction.South:
			roomHeight = Mathf.Clamp (roomHeight, 1, corridorss.EndPosY);

			yPos = corridorss.EndPosY - roomHeight + 1;
			xPos = Random.Range (corridorss.EndPosX - roomWidth + 1, corridorss.EndPosX);

			xPos = Mathf.Clamp (xPos, 0, mmapWidth - roomWidth);
			break;

		case Direction.East:
			roomWidth = Mathf.Clamp (roomWidth, 1, mmapWidth - corridorss.EndPosX);

			yPos = Random.Range (corridorss.EndPosY - roomHeight + 1, corridorss.EndPosY);
			xPos = corridorss.EndPosX;

			yPos = Mathf.Clamp (yPos,0,mmapHeight - roomHeight);		
			break; */

		case Direction.North:

			roomHeight = Mathf.Clamp (roomHeight, 1, mmapHeight - corridorss.EndPosY);
			yPos = corridorss.EndPosY;
			xPos = Random.Range (corridorss.EndPosX - roomWidth + 1, corridorss.EndPosX);
			xPos = Mathf.Clamp (xPos, 0, mmapWidth - roomWidth);
			break;

		case Direction.East:

			roomWidth = Mathf.Clamp (roomWidth, 1, mmapWidth - corridorss.EndPosX);
			xPos = corridorss.EndPosX;

			yPos = Random.Range (corridorss.EndPosY - roomHeight + 1, corridorss.EndPosY);
			yPos = Mathf.Clamp (yPos, 0, mmapHeight - roomHeight);
			break;

		case Direction.South:

			roomHeight = Mathf.Clamp (roomHeight, 1, corridorss.EndPosY);
			yPos = corridorss.EndPosY - roomHeight + 1;

			xPos = Random.Range (corridorss.EndPosX - roomWidth + 1, corridorss.EndPosX);
			xPos = Mathf.Clamp (xPos, 0, mmapWidth - roomWidth);
			break;


		case Direction.West:

			roomWidth = Mathf.Clamp (roomWidth, 1, corridorss.EndPosX);
			xPos = corridorss.EndPosX - roomWidth + 1;

			yPos = Random.Range (corridorss.EndPosY - roomHeight + 1, corridorss.EndPosY);
			yPos = Mathf.Clamp (yPos, 0, mmapHeight - roomHeight);
			break;
		}

	}

	}
