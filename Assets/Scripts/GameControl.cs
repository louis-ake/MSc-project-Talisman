using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	
	// Used to manage and control the game

	// Use this for initialization
	void Start ()
	{
		DirectionDecision.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (!Finished && DiceRoll.RollCount > 1)
		{
			Main();	
		}
		if (BluePlayer.CurrentPos == BluePlayer.EndPos)
		{
			Finished = true;
		}
	}
	

	public Transform Target;

	public static bool Finished = false;

	public static int TurnCount = 0;

	public Text DirectionDecision;
	
	
	void Main()
	{
		MoveBluePlayer();
		//MoveBluePlayer();
		//Finished = true;
	}

	void MoveBluePlayer()
	{
		string CurrentTile = BluePlayer.StartTileName;
		int CurrentTileNo = Convert.ToInt32(CurrentTile.Substring(1));
		int NextTileNo = 0;
		DirectionDecision.text = "Press c to move clockwise or v to move anticlockwise";
		if (Input.GetKey(KeyCode.C)) // For clockwise
		{
			NextTileNo = (CurrentTileNo + DiceRoll.DiceTotal);
			TurnCount += 1;
		}
		else if (Input.GetKey(KeyCode.V)) // For anticlockwise
		{
			NextTileNo = (CurrentTileNo - DiceRoll.DiceTotal);
			TurnCount += 1;
		}
		// Manual implementation of modulo as did not work when integrated into above loops
		if (NextTileNo != 0 && TurnCount == DiceRoll.RollCount)
		{
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
			BluePlayer.SetEndPos(new Vector2(tx, ty));
			BluePlayer.SetStartTileName(nextTileName);
			BluePlayer.Active = true;
		}
	}
}
