using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayer : Player {

	// Use this for initialization
	void Start ()
	{
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(15, -15);
		CurrentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// this must be updated every frame for algorithm to work
		if (!Active) return;
		CurrentPos = this.transform.position;
		if (Vector2.Distance(new Vector2(CurrentPos.x, CurrentPos.y), EndPos) > 0)
		{
			this.transform.position = Vector2.MoveTowards(CurrentPos, EndPos, Speed);
		}
	}

	public static string StartTileName = "O1";
	
	public static int Turns = 0;

	public static Vector2 EndPos;
	public static Vector2 CurrentPos;
	
	public static bool Active = false;

	public static int NoOfMoves;
	
	public static Transform Target;

	public static void SetEndPos(Vector2 pos)
	{
		EndPos = pos;
	}
	

	public static void SetStartTileName(string name)
	{
		StartTileName = name;
	}


	public static void Move()
	{
		// check there has been the correct number of rolls to caluclate move
		if (GameControl.TurnCount != DiceRoll.RollCount - 1) {return;}
		string CurrentTile = StartTileName;
		int CurrentTileNo = Convert.ToInt32(CurrentTile.Substring(1));
		int NextTileNo = 0;
		// GameControl.DirectionDecision.text = "Press c to move clockwise or v to move anticlockwise";
		if (Input.GetKey(KeyCode.C)) // For clockwise
		{
			NextTileNo = (CurrentTileNo + DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		else if (Input.GetKey(KeyCode.V)) // For anticlockwise
		{
			NextTileNo = (CurrentTileNo - DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		// check ratio of rolls and move calucluations 
		if (GameControl.TurnCount != DiceRoll.RollCount) return;
		// Manual implementation of modulo as did not work when integrated into above loops
		if (NextTileNo < 1) { NextTileNo += 23; }
		if (NextTileNo > 24) { NextTileNo -= 23; }
		Debug.Log("next tile's number is: " + NextTileNo);
		string nextTileName = "O" + NextTileNo.ToString();
		Debug.Log("next tile's name is: " + nextTileName);
		GameObject NextTile = GameObject.Find(nextTileName);
		Target = NextTile.transform;
		float tx = Target.position.x;
		float ty = Target.position.y;
		Debug.Log("x = " + tx);
		Debug.Log("y = " + ty);
		SetEndPos(new Vector2(tx, ty));
		SetStartTileName(nextTileName);
		Turns += 1;
		// So that a move is not attempted before game is set up
		Active = true;
	}

}
