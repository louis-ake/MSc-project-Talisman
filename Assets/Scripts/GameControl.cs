using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameControl : MonoBehaviour {
	
	// Used to manage and control the game

	// Use this for initialization
	void Start ()
	{
		SetAlignments();
		Turn.text = turnString;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Finished)
		{
			Main();	
		}
		Turn.text = turnString;
	}

	public Text Turn;
	private static string turnString;

	static bool Finished = false;

	public static int TurnCount = 0;

	private const int StrengthUpgrade = 4;

	private const int StartingFate = 2;
	
	// 0 for blue player; 1 for yellow
	public static int TurnTracker = 0;


	public static void SetTurnText()
	{
		turnString = TurnTracker == 0 ? "Blue Player's turn" : "Yellow Player's turn";
	}

	public static void AlternateTurnTracker()
	{
		TurnTracker = TurnTracker == 0 ? 1 : 0;
		if (BluePlayer.lives < 1 || YellowPlayer.lives < 1)
		{
			EndGame();
		}
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
			BluePlayer.strengthTrophy -= StrengthUpgrade;
		}
		else
		{
			YellowPlayer.strengthTrophy += change;
			if (YellowPlayer.strengthTrophy < StrengthUpgrade) return;
			YellowPlayer.strength += 1;
			YellowPlayer.strengthTrophy -= StrengthUpgrade;
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

	public static int GetFate()
	{
		return TurnTracker == 0 ? BluePlayer.fateTokens : YellowPlayer.fateTokens;
	}

	public static int GetLives()
	{
		return TurnTracker == 0 ? BluePlayer.lives : YellowPlayer.lives;
	}

	public static void GiveTalisman()
	{
		if (TurnTracker == 0)
		{
			BluePlayer.talisman = "yes";
		}
		else
		{
			YellowPlayer.talisman = "yes";
		}
	}


	public static bool CheckTalisman()
	{
		if (TurnTracker == 0)
		{
			return BluePlayer.talisman == "yes";
		}
		else
		{
			return YellowPlayer.talisman == "yes";
		}
	}

	/**
	 * Randomly allocate each player good or evil, exclusively 
	 */
	private static void SetAlignments()
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


	public static void EndGame()
	{
		Player.Decision = TurnTracker == 0 ? "Blue Player wins!" : "Yellow Player wins!";
		AdventureDeck._deckText = "Game ended and reset";
		BluePlayer.MoveRegion("O", 24, "O1");
		BluePlayer.lives = 3;
		BluePlayer.strength = 4;
		BluePlayer.strengthTrophy = 0;
		BluePlayer.fateTokens = 2;
		BluePlayer.gold = 4;
		BluePlayer.alignment = "";
		BluePlayer.Turns = 0;
		YellowPlayer.MoveRegion("O", 24, "O13");
		YellowPlayer.lives = 3;
		YellowPlayer.strength = 4;
		YellowPlayer.strengthTrophy = 0;
		YellowPlayer.fateTokens = 2;
		YellowPlayer.gold = 4;
		YellowPlayer.alignment = "";
		YellowPlayer.Turns = 0;
		SetAlignments();
		TurnCount = 0;
		DiceRoll.RollCount = 0;
		TurnTracker = 0;
	}

	/**
	 * Calls the respective player's TakeTurn method based on the
	 * TurnTracker value. Called in this class's Update method.
	 */
	private static void Main()
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
