using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameControl : MonoBehaviour {
	
	// Used to manage and control the game

	// Use this for initialization
	void Start ()
	{
		SetAlignments();
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

	private void SetAlignments()
	{
		var result = Random.Range(1, 3);
		if (result == 1)
		{
			BluePlayer.alignment = "good";
			YellowPlayer.alignment = "evil";
		}
		else
		{
			BluePlayer.alignment = "evil";
			YellowPlayer.alignment = "good";
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
