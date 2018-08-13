using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class GameControl : MonoBehaviour {
	
	// Used to manage and control the game

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameFinished)
		{
			Main();	
		}
	}

	public Transform target;

	public bool gameFinished = false;
	

	void Main() 
	{
		{
			/*GameObject o2 = GameObject.Find("O2");
			target = o2.transform;
			float tx = target.position.x;
			float ty = target.position.y;
			Debug.Log("x = " + tx);
			Debug.Log("y = " + ty);
			Player.SetEndPos(new Vector2(tx, ty));*/
			string CurrentTile = BluePlayer.StartTileName;
			int CurrentTileNo = Convert.ToInt32(CurrentTile.Substring(1));
			int NextTileNo = 0;
			// For clockwise
			if (Input.GetKey(KeyCode.C))
			{
				NextTileNo = (CurrentTileNo + DiceRoll.DiceTotal) % 24;
			}
			// For anticlockwise
			else if (Input.GetKey(KeyCode.V))
			{
				NextTileNo = (CurrentTileNo - DiceRoll.DiceTotal) % 24;
			}
			string nextTileName = "0" + NextTileNo.ToString();
			GameObject o2 = GameObject.Find(nextTileName);
			target = o2.transform;
			float tx = target.position.x;
			float ty = target.position.y;
			Debug.Log("x = " + tx);
			Debug.Log("y = " + ty);
			BluePlayer.SetEndPos(new Vector2(tx, ty));
			BluePlayer.SetStartTileName(nextTileName);
			if (BluePlayer.currentPos == BluePlayer.endPos)
			{
				gameFinished = true;
			}
		}
	}
}
