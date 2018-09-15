using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameControl : MonoBehaviour {
	
	/**
	 * Used to manage and control the game. Methods to get and change all
	 * player stats, Main method which calls the correct player's turn,
	 * method to end and reset the game.
	 */

	// Use this for initialization
	void Start ()
	{
		SetAlignments();
		Turn.text = _turnString;
	}
	
	// Update is called once per frame
	void Update () {
		Main();
		Turn.text = _turnString;
	}

	// To display which player's turn it is on-screen
	public Text Turn;
	private static string _turnString;

	public static int TurnCount = 0;

	// Strength trophy needed to gain 1 strength
	private const int StrengthUpgrade = 5;
	
	// 0 for blue player; 1 for yellow
	public static int TurnTracker = 0;


	public static void SetTurnText()
	{
		_turnString = TurnTracker == 0 ? "Blue Player's turn" : "Yellow Player's turn";
	}

	public static void AlternateTurnTracker()
	{
		TurnTracker = TurnTracker == 0 ? 1 : 0;
		if (BluePlayer.lives >= 1 && YellowPlayer.lives >= 1) return;
		Player.Decision = BluePlayer.lives < 1 ? "Blue player lost last life" : "Yellow player lost last life";
		EndGame();
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
			BluePlayer.fateTokens = Player.StatingFateTokens;
		}
		else
		{
			YellowPlayer.fateTokens = Player.StatingFateTokens;
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
		switch (TurnTracker)
		{
			case 0:
				return BluePlayer.talisman == "yes";
			case 1:
				return YellowPlayer.talisman == "yes";
			default:
				return false;
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


	/**
	 * Resets the game
	 */
	public static void EndGame()
	{
		if (BluePlayer.lives < 1) { YellowPlayer.Wins += 1; }
		else { BluePlayer.Wins += 1; }
		AdventureDeck._deckText = "Game ended and reset";
		BluePlayer.MoveRegion("O", 24, "O1");
		BluePlayer.lives = Player.StartingLives;
		BluePlayer.strength = Player.StartingStrength;
		BluePlayer.strengthTrophy = 0;
		BluePlayer.fateTokens = Player.StatingFateTokens;
		BluePlayer.gold = Player.StartingGold;
		BluePlayer.alignment = "";
		BluePlayer.Turns = 0;
		YellowPlayer.MoveRegion("O", 24, "O13");
		YellowPlayer.lives = Player.StartingLives;
		YellowPlayer.strength = Player.StartingStrength;
		YellowPlayer.strengthTrophy = 0;
		YellowPlayer.fateTokens = Player.StatingFateTokens;
		YellowPlayer.gold = Player.StartingGold;
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
