using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
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

	private const int StrengthUpgrade = 4;

	private const int StartingFate = 2;
	
	
	// 0 for blue player; 1 for yellow
	public static int TurnTracker = 0;

	public static void AlternateTurnTracker()
	{
		TurnTracker = TurnTracker == 0 ? 1 : 0;
	}

	public static void ChangeLives(int change)
	{
		if (TurnTracker == 0)
		{
			BluePlayer.lives += change;
		}
		else
		{
			YellowPlayer.lives += change;
		}
	}

	public static void ChangeStrength(int change)
	{
		if (TurnTracker == 0)
		{
			BluePlayer.strength += change;
		}
		else
		{
			YellowPlayer.strength += change;
		}
	}

	public static void ChangeStrengthTrophy(int change)
	{
		if (TurnTracker == 0)
		{
			BluePlayer.strengthTrophy += change;
			if (BluePlayer.strengthTrophy < StrengthUpgrade) return;
			BluePlayer.strength += 1;
			BluePlayer.strengthTrophy -= BluePlayer.strengthTrophy;
		}
		else
		{
			YellowPlayer.strengthTrophy += change;
			if (YellowPlayer.strengthTrophy < StrengthUpgrade) return;
			YellowPlayer.strength += 1;
			YellowPlayer.strengthTrophy -= BluePlayer.strengthTrophy;
		}
	}


	public static void ChangeGold(int change)
	{
		if (TurnTracker == 0)
		{
			BluePlayer.gold += change;
		}
		else
		{
			YellowPlayer.gold += change;
		}
	}
	

	public static void ChangeFate(int change)
	{
		if (TurnTracker == 0)
		{
			BluePlayer.fateTokens += change;
		}
		else
		{
			YellowPlayer.fateTokens += change;
		}
	}


	public static void ReplenishFate()
	{
		if (TurnTracker == 0)
		{
			BluePlayer.fateTokens = StartingFate;
		}
		else
		{
			YellowPlayer.fateTokens = StartingFate;
		}
	}

	public static int GetStrength()
	{
		return TurnTracker == 0 ? BluePlayer.strength : YellowPlayer.strength;
	}


	public static int GetGold()
	{
		return TurnTracker == 0 ? BluePlayer.gold : YellowPlayer.gold;
	}

	public static int GetLives()
	{
		return TurnTracker == 0 ? BluePlayer.lives : YellowPlayer.lives;
	}

	public static void GiveTalisman()
	{
		if (TurnTracker == 1)
		{
			BluePlayer.talisman = "yes";
		}
		else
		{
			YellowPlayer.talisman = "no";
		}
	}

	/**
	 * Randomly allocate each player good or evil, exclusively 
	 */
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
		switch (TurnTracker)
		{
			case 0:
				BluePlayer.TakeTurn();
				break;
			case 1:
				YellowPlayer.TakeTurn();
				break;
		}
	}
}
