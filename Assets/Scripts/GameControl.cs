﻿using System;
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
	
	
	void Main()
	{
		if (BluePlayer.Turns == YellowPlayer.Turns)
		{
			BluePlayer.Move();	
			// AdventureDeck.FightBandit(BluePlayer.strength);
		}
		else
		{
			YellowPlayer.Move();
			// AdventureDeck.FightBandit(YellowPlayer.strength);
		}
		
	}
}
