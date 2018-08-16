﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	// Each players starting stats - currently the same for all
	public int lives = 3;
	public int strength = 4;
	public int craft = 4;
	public int darkFate = 2;
	public int lightFate = 2;
	
	// number of seconds to complete move
	public float Speed = 1f;


	/*public static string StartTileName = "O1";

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
		Active = true;
	}*/

}
