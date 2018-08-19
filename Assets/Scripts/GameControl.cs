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
	}
	
	// Update is called once per frame
	void Update () {
		if (!Finished)
		{
			Main();	
		}
	}
	

	static bool Finished = false;

	public static int TurnCount = 0;
	
	
	// 0 for blue player; 1 for yellow
	public static int TurnTracker = 0;

	public static void AlternateTurnTracker()
	{
		if (TurnTracker == 0)
		{
			TurnTracker = 1;
		}
		else
		{
			TurnTracker = 0;
		}
	}

	public static void ReduceLives()
	{
		if (TurnTracker == 0)
		{
			BluePlayer.lives -= 1;
		}
		else
		{
			YellowPlayer.lives -= 1;
		}
	}
	
	
	void Main()
	{
		if (TurnTracker == 0)
		{
			BluePlayer.TakeTurn();	
		}
		else if (TurnTracker == 1)
		{
			YellowPlayer.TakeTurn();
		}
		
	}
}
