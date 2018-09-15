using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Xsl;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using Random = System.Random;

public class BluePlayer : Player {
	
	/**
	 * The player controlled by a human
	 */

	// Use this for initialization
	void Start ()
	{
		// Initialise this player in the bottom-right Tile
		transform.position = new Vector2(15, -15);
		_currentPos = transform.position;
		SetStats();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_active) return;
		_currentPos = transform.position;
		if (Vector2.Distance(new Vector2(_currentPos.x, _currentPos.y), _endPos) > 0)
		{
			transform.position = Vector2.MoveTowards(_currentPos, _endPos, Speed); // movement
		}
		SetStats();
	}
	
	
	// players' starting stats 
	public static int lives = StartingLives;
	public static int strength = StartingStrength;
	public static int strengthTrophy = 0;
	public static int fateTokens = StatingFateTokens;
	public static int gold = StartingGold;
	public static string alignment = "";
	public static string talisman = "no";
	public static int Wins = 0;

	private static string _startTileName = "O1";
	
	// So that movement works correctly in different regions
	private static string _region = "O"; // O/M/I/C
	private static int _regionUpperBound = 24; // 24/16/8/1
	
	public static int Turns = 0;

	private static Vector2 _endPos;
	private static Vector2 _currentPos;
	
	private static Transform _target;

	// is the player active yet - triggered after first movement
	private static bool _active;

	// On-screen text object to display player stats
	public Text Stats;

	private void SetStats()
	{
		Stats.text = "Blue Player's Stats" + "\n"+ "\n" + 
		             "lives: " + lives + "\n" + 
		             "strength: " + strength + " (" + strengthTrophy + ")" + "\n" + 
		             "fate tokens: " + fateTokens + "\n" + 
		             "gold: " + gold + "\n" +  
		             "alignment: " + alignment + "\n" + 
		             "talisman: " + talisman + "\n" + "\n" +
		             "turns: " + Turns + "\n" +
		             "wins: " + Wins;
	}


	private static void SetEndPos(Vector2 pos)
	{
		_endPos = pos;
	}


	private static void SetStartTileName(string name)
	{
		_startTileName = name;
	}

	/**
	 * The player turn's central method. Calls movement and then using the
	 * player's current space as an identifier checks which method below
	 * should be called next to get the correct method for that space,
	 * and calls that.
	 */
	public static void TakeTurn()
	{
		Move();
		if (Turns != YellowPlayer.Turns + 1) return;
		if (AdventureDeck.AllCardTiles.Contains(_startTileName))
		{
			DrawFromDeck();
		} else if (UniqueTiles.LifeLossDraw.Contains(_startTileName))
		{
			GameControl.ChangeLives(-1);
			DrawFromDeck();
		} else if (UniqueTiles.FightTiles.Contains(_startTileName))
		{
			EncounterUniqueFightTile();
		} else if (UniqueTiles.ArmouryTiles.Contains(_startTileName))
		{
			EncounterArmouryTile();
			ActionNeeded = false;
		} else if (UniqueTiles.HealTiles.Contains(_startTileName))
		{
			EncounterHealTile();
			ActionNeeded = false;
		} else if (UniqueTiles.Tiles.Contains(_startTileName))
		{
			UniqueTiles.ChooseTile(_startTileName);
			ActionNeeded = false;
			GameControl.AlternateTurnTracker();
		}		
	}
	
	/**
	 * Called by Taketurn when player lands on space that entails drawing from the
	 * adventure deck. Fate can be used.
	 */
	private static void DrawFromDeck() {	
		if (Moved && ActionNeeded)
		{
			FightDiff = AdventureDeck.ProduceCard(_startTileName);
			ActionNeeded = false;
		}
		if (!Done && Moved) 
		{
			UseFate(FightDiff);
		}
	}
	

	/**
	 * Called by TakeTurn is the unique space involves fighting an enemy.
	 * Fate can be used.
	 */
	private static void EncounterUniqueFightTile()
	{
		if (Moved && ActionNeeded)
		{
			FightDiff = UniqueTiles.ChooseFightTile(_startTileName);
			ActionNeeded = false;
		}
		if (!Done && Moved)
		{
			UseFate(FightDiff);
		}
	}

	
	/**
	 * Called by TakeTurn when the player lands an armoury space.
	 * Asks player whether they'd like to purchase improved armaments.
	 * If player answers yes, makes relevant changes to player stats.
	 */
	private static void EncounterArmouryTile()
	{
		if (gold < 2) // insufficient gold
		{
			GameControl.AlternateTurnTracker();
			return;
		}
		AdventureDeck._deckText = "Would you like to improve your armaments for 2 gold?";
		if (Input.GetKey(KeyCode.Y))
		{
			GameControl.ChangeStrength(1);
			GameControl.ChangeGold(-2);
			GameControl.AlternateTurnTracker();
		} else if (Input.GetKey(KeyCode.N))
		{
			GameControl.AlternateTurnTracker();
		}
	}

	
	/**
	 * Called by Taketurn when the player lands on a heal space.
	 * Asks the player if they'd like to pay to be healed.
	 * If yes, makes the relevant changes to player stats.
	 */
	private static void EncounterHealTile()
	{
		if (gold < 1) // insufficient gold
		{
			GameControl.AlternateTurnTracker();
			return;
		}
		AdventureDeck._deckText = "Would you like to heal 1 life for 1 gold?";		
		if (Input.GetKey(KeyCode.Y))
		{
			GameControl.ChangeLives(1);
			GameControl.ChangeGold(-1);
			GameControl.AlternateTurnTracker();
		} else if (Input.GetKey(KeyCode.N))
		{
			GameControl.AlternateTurnTracker();

		}
		
	}
	
	/**
	 * takes the difference between the player and enemy result as an integer
	 * and rolls one 6-sided dice to try and make up the difference. If
	 * successful, gain back a life.
	 */
	private static void UseFate(int diff)
	{
		if (fateTokens < 1) // insuffienient fate tokens
		{
			Done = true;
			GameControl.AlternateTurnTracker();
			return;
		}
		Decision = "Would you like to use a fate token? (y/n)";
		if (Input.GetKey(KeyCode.Y))
		{
			var challenge = UnityEngine.Random.Range(1, 7);
			var result = challenge + diff;
			if (result >= 0)
			{
				GameControl.ChangeLives(1);
				Decision = "Fate token used and Successful! (rolled = " + challenge + ")";
			}
			else
			{
				Decision = "Fate token used and Unsuccessful (rolled = " + challenge + ")";
			}
			Done = true;
			GameControl.ChangeFate(-1);
			GameControl.AlternateTurnTracker();
		} else if (Input.GetKey(KeyCode.N))
		{
			Done = true;
			GameControl.AlternateTurnTracker();
		}
	}
	
	
	/**
	 * For moving between regions only.
	 * Sets the region movement variables to the relevant ones
	 * and moves player to target space in other region.
	 */
	public static void MoveRegion(string region, int regionTiles, string tileName)
	{
		_region = region;
		_regionUpperBound = regionTiles;
		var nextTile = GameObject.Find(tileName);
		_target = nextTile.transform;
		var tx = _target.position.x;
		var ty = _target.position.y;
		SetEndPos(new Vector2(tx, ty));
		SetStartTileName(tileName);
	}
	

	/**
	 * For movement at the start of a turn only.
	 * Uses the last DiceRoll value to calculate how many spaces will be moved.
	 */
	private static void Move()
	{
		// check there has been the correct number of rolls to caluclate move
		if (GameControl.TurnCount != DiceRoll.RollCount - 1) return;
		if (Turns != YellowPlayer.Turns) return;
		Moved = false; 
		var currentTile = _startTileName;
		var currentTileNo = Convert.ToInt32(currentTile.Substring(1));
		var nextTileNo = 0;
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
		if (nextTileNo < 1) { nextTileNo += _regionUpperBound; }
		if (nextTileNo > _regionUpperBound) { nextTileNo -= _regionUpperBound; }
		Debug.Log("next tile's number is: " + nextTileNo);
		var nextTileName = _region + nextTileNo.ToString();
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
		Moved = true;
		ActionNeeded = true;
	}
}