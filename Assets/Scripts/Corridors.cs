using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	North, West, South, East,
}

public class Corridors {
	
	public int startX;
	public int startY;
	public int length;
	public Direction direction;

	public int EndPosX {
	
		get {

			if (direction == Direction.North || direction == Direction.South)
				return startX;
			if (direction == Direction.East)
				return startX + length - 1;
			
			return startX - length + 1;
		}
	
	}

	public int EndPosY {

		get {

			if (direction == Direction.West || direction == Direction.East)
				return startY;

			if (direction == Direction.North) 
				return startY + length - 1;
			//if (direction == Direction.South)
				return startY - length;
		}

	}

	public void SetupCorridors(Rooms rooms, IntRange Tlength, IntRange roomWidth, IntRange roomHeight, int mapWidth, int mapHeight, bool FirstCorridor) {
		
		direction = (Direction)Random.Range(0, 4);
		Direction oppositeDirection = (Direction)(((int)rooms.enteringCorridor + 2) % 4);
	
		if (!FirstCorridor && direction == oppositeDirection) {
		
			int temp = (int)direction;
			temp++;
			temp = temp % 4;
			direction = (Direction)temp;

		}

		length = Tlength.Random;

		int maxLength = Tlength.m_Max;
		//while (1) {
			//try {




		switch (direction) {
		case Direction.North:
			startX = Random.Range (rooms.xPos, rooms.xPos + rooms.roomWidth - 1);
			startY = rooms.yPos + rooms.roomHeight - 1;
			maxLength = mapHeight - startY - roomHeight.m_Min;
			break;
		case Direction.South:
			startX = Random.Range (rooms.xPos, rooms.xPos + rooms.roomWidth);
			startY = rooms.yPos;
			maxLength = startY - roomHeight.m_Min;
			break;
		case Direction.West:
			startX = rooms.xPos;
			startY = Random.Range (rooms.yPos, rooms.yPos + rooms.roomHeight - 1);
			maxLength = startX - roomWidth.m_Min;
			break;
		case Direction.East:
			startX = rooms.xPos + rooms.roomWidth - 1;
			startY = Random.Range (rooms.yPos, rooms.yPos + rooms.roomHeight - 1);
			maxLength = mapWidth - startX - roomWidth.m_Min;
			break;
		}
		//	}

				//	except OutOfRangeException
				
		length = Mathf.Clamp (length, 1, maxLength);

			
	}

}
