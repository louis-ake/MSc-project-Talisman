using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayer : Player {
	
	/**
	 * BluePlayer and YellowPlayer mirror each other appart from starting transform
	 * and StartTileName string.
	 */

	// Use this for initialization
	void Start () {
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(-15, 15);
		_currentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// this must be updated every frame for algorithm to work
		if (!_active) return;
		_currentPos = this.transform.position;
		if (Vector2.Distance(new Vector2(_currentPos.x, _currentPos.y), _endPos) > 0)
		{
			this.transform.position = Vector2.MoveTowards(_currentPos, _endPos, Speed);
		}
		
	}


	private static string _startTileName = "O13";

	public static int Turns = 0;

	private static Vector2 _endPos;
	private static Vector2 _currentPos;

	private static bool _active = false;

	public static int NoOfMoves;

	private static Transform _target;

	private static void SetEndPos(Vector2 pos)
	{
		_endPos = pos;
	}


	private static void SetStartTileName(string name)
	{
		_startTileName = name;
	}


	public static void Move()
	{
		// check there has been the correct number of rolls to caluclate move
		if (GameControl.TurnCount != DiceRoll.RollCount - 1) {return;}
		var currentTile = _startTileName;
		var currentTileNo = Convert.ToInt32(currentTile.Substring(1));
		var nextTileNo = 0;
		// GameControl.DirectionDecision.text = "Press c to move clockwise or v to move anticlockwise";
		if (Input.GetKey(KeyCode.C)) // For clockwise
		{
			nextTileNo = (currentTileNo + DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		else if (Input.GetKey(KeyCode.V)) // For anticlockwise
		{
			nextTileNo = (currentTileNo - DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		// check ratio of rolls and move calucluations 
		if (GameControl.TurnCount != DiceRoll.RollCount) return;
		// Manual implementation of modulo as did not work when integrated into above loops
		if (nextTileNo < 1) { nextTileNo += 23; }
		if (nextTileNo > 24) { nextTileNo -= 23; }
		Debug.Log("next tile's number is: " + nextTileNo);
		var nextTileName = "O" + nextTileNo.ToString();
		Debug.Log("next tile's name is: " + nextTileName);
		var nextTile = GameObject.Find(nextTileName);
		_target = nextTile.transform;
		var tx = _target.position.x;
		var ty = _target.position.y;
		Debug.Log("x = " + tx);
		Debug.Log("y = " + ty);
		SetEndPos(new Vector2(tx, ty));
		SetStartTileName(nextTileName);
		Turns += 1;
		// So that a move is not attempted before game is set up
		_active = true;
	}
}
