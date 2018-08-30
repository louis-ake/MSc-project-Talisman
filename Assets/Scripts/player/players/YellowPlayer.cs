﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;

public class YellowPlayer : Player {
	
	/**
	 * BluePlayer and YellowPlayer mirror each other appart from starting transform,
	 * StartTileName string and turnTracker value.
	 */

	// Use this for initialization
	void Start () {
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(-15, 15);
		_currentPos = this.transform.position;
		SetStats();
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
		SetStats();
		
	}
	
	/*public static bool moved;
	public static bool actionNeeded;*/ 
	
	
	// Each players starting stats - currently the same for all
	public static int lives = StartingLives;
	public static int strength = StartingStrength;
	public static int strengthTrophy = 0;
	public static int fateTokens = StatingFateTokens;
	public static int gold = StartingGold;
	public static string alignment = "";
	public static string talisman = "no";

	private static string _startTileName = "O13";
	
	// So that movement works correctly in different regions
	public static string Region = "O"; // O/M/I/C
	public static int RegionUpperBound = 24; // 24/16/8/1
	
	public static int Turns = 0;

	private static Vector2 _endPos;
	private static Vector2 _currentPos;

	private static bool _active = false;

	private static Transform _target;

	public Text Stats;

	private void SetStats()
	{
		Stats.text = "Yellow Player's Stats" + "\n"+ "\n" + 
		             "lives: " + lives + "\n" + 
		             "strength: " + strength + " (" + strengthTrophy + ")" + "\n" + 
		             "fate tokens: " + fateTokens + "\n" + 
		             "gold: " + gold + "\n" +  
		             "alignment: " + alignment + "\n" + 
		             "talisman: " + talisman + "\n" + "\n" +
		             "turns: " + Turns;
	}


	public static void SetEndPos(Vector2 pos)
	{
		_endPos = pos;
	}


	private static void SetStartTileName(string name)
	{
		_startTileName = name;
	}


	public static void TakeTurn()
	{
		if (won)
		{
			Move();
		}
		if (!_active) return;
		if (AdventureDeck.AllCardTiles.Contains(_startTileName))
		{
			DrawFromDeck();
		}
		else if (UniqueTiles.FightTiles.Contains(_startTileName))
		{
			EncounterUniqueFightTile();
		}
		else if (UniqueTiles.ArmouryTiles.Contains(_startTileName) &&
		         GameControl.GetGold() >= UniqueTiles.ArmouryPrice)
		{
			EncounterArmouryTile();
			actionNeeded = false;
		}
		else if (UniqueTiles.HealTiles.Contains(_startTileName) &&
		         GameControl.GetGold() >= UniqueTiles.HealPrice)
		{
			EncounterHealTile();
			actionNeeded = false;
		}
		else if (UniqueTiles.LifeLossDraw.Contains(_startTileName))
		{
			DrawFromDeck();
			GameControl.ChangeLives(-1);
		}
		else if (UniqueTiles.Tiles.Contains(_startTileName) && moved && actionNeeded)
		{
			UniqueTiles.ChooseTile(_startTileName);
			actionNeeded = false;
			GameControl.AlternateTurnTracker();
		}
		else if (moved && actionNeeded)
		{
			actionNeeded = false;
			GameControl.AlternateTurnTracker();
		}

	}

	/**
	 * Must be handled separately as need return values and particular control
	 * flow to allow use of fate
	 */
	private static void EncounterUniqueFightTile()
	{
		if (moved && !done && actionNeeded)
		{
			FightDiff = UniqueTiles.ChooseFightTile(_startTileName);
			actionNeeded = false;
		}
		if (!won && !done && moved)
		{
			UseFate(FightDiff);
		} else if (won && moved && done)
		{
			//GameControl.AlternateTurnTracker();
			done = false;
		}
	}

	public static void MoveRegion(string region, int regionTiles, string tileName)
	{
		Region = region;
		RegionUpperBound = regionTiles;
		var nextTile = GameObject.Find(tileName);
		_target = nextTile.transform;
		var tx = _target.position.x;
		var ty = _target.position.y;
		SetEndPos(new Vector2(tx, ty));
		SetStartTileName(tileName);
	}

	private static void DrawFromDeck() {	
		if (moved && !done && actionNeeded)
		{
			FightDiff = AdventureDeck.ProduceCard(_startTileName);
			actionNeeded = false;
		}
		if (!won && !done && moved)
		{
			UseFate(FightDiff);
		} else if (won && moved && done)
		{
			//GameControl.AlternateTurnTracker();
			done = false;
		}
	}
	
	
	private static void EncounterArmouryTile()
	{
		AdventureDeck._deckText = "Would you like to improve your armaments for 2 gold?";
		if (Input.GetKey(KeyCode.Y))
		{
			GameControl.ChangeStrength(1);
			GameControl.ChangeGold(-2);
			GameControl.AlternateTurnTracker();
			done = true;
			won = true;
		} else if (Input.GetKey(KeyCode.N))
		{
			GameControl.AlternateTurnTracker();
			done = true;
			won = true;
		}
	}


	private static void EncounterHealTile()
	{
		AdventureDeck._deckText = "Would you like to heal 1 life for 1 gold?";		
		if (Input.GetKey(KeyCode.Y))
		{
			GameControl.ChangeLives(1);
			GameControl.ChangeGold(-1);
			GameControl.AlternateTurnTracker();
			done = true;
			won = true;
			
		} else if (Input.GetKey(KeyCode.N))
		{
			GameControl.AlternateTurnTracker();
			done = true;
			won = true;
		}
		
	}
	
	/**
	 * takes the difference between the player and enemy result as an integer
	 * and rolls one 6-sided dice to try and make up the difference. If
	 * successful, gain back a life.
	 */
	private static void UseFate(int diff)
	{
		Decision = "Would you like to use a fate token? (y/n)";
		if (Input.GetKey(KeyCode.Y))
		{
			var challenge = Random.Range(1, 7);
			var result = challenge - diff;
			if (result >= 0)
			{
				GameControl.ChangeLives(1);
				Decision = "Successful! (rolled = " + challenge + ")";
			}
			else
			{
				Decision = "Unsuccessful (rolled = " + challenge + ")";
			}
			won = true;
			GameControl.ChangeFate(-1);
			GameControl.AlternateTurnTracker();
		} else if (Input.GetKey(KeyCode.N))
		{
			won = true;
			GameControl.AlternateTurnTracker();
		}
	}

	private static void Move()
	{
		// check there has been the correct number of rolls to caluclate move
		if (GameControl.TurnCount != DiceRoll.RollCount - 1) {return;}
		moved = false; 
		var currentTile = _startTileName;
		var currentTileNo = Convert.ToInt32(currentTile.Substring(1));
		var clockwise = currentTileNo + DiceRoll.DiceTotal;
		var antiClockwise = currentTileNo - DiceRoll.DiceTotal;
		var nextTileNo = 0;
		// GameControl.DirectionDecision.text = "Press c to move clockwise or v to move anticlockwise";
		if (AIChooseDirection(currentTileNo, clockwise, antiClockwise)) // For clockwise
		{
			nextTileNo = (currentTileNo + DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		else // For anticlockwise
		{
			nextTileNo = (currentTileNo - DiceRoll.DiceTotal);
			GameControl.TurnCount += 1;
		}
		// check ratio of rolls and move calucluations 
		if (GameControl.TurnCount != DiceRoll.RollCount) return;
		// Manual implementation of modulo as did not work when integrated into above loops
		if (nextTileNo < 1) { nextTileNo += RegionUpperBound; }
		if (nextTileNo > RegionUpperBound) { nextTileNo -= RegionUpperBound; }
		Debug.Log("next tile's number is: " + nextTileNo);
		var nextTileName = Region + nextTileNo.ToString();
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
		moved = true;
		actionNeeded = true;
	}



	private static bool AIChooseDirection(int currentTileNo, int clockwise, int antiClockwise)
	{
		if (Region == "O")
		{
			if (GameControl.GetStrength() > 7) // Move towards sentinal
			{
				const int target = 5;
				return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
			}
			
			if (GameControl.GetGold() > 2) // Go to city
			{
				const int target = 13;
				return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
			}

			if (fateTokens < StatingFateTokens) // Replish fate
			{
				if (alignment == "good") // at temple
				{
					const int target = 7;
					return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
				} else if (alignment == "evil") // at graveyard
				{
					const int target = 3;
					return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
				}
			}
		} else if (Region == "M")
		{
			if (!GameControl.CheckTalisman()) // Get talisman at warlocks's cave
			{
				const int target = 9;
				return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
			}

			if (GameControl.GetLives() < 3 && GameControl.GetGold() < 1) // Heal at castle
			{
				const int target = 16;
				return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
			}

			if (GameControl.GetStrength() > 10) // go to portal
			{
				const int target = 1;
				return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
			}
		} else if (Region == "I") // got to valley
		{
			const int target = 5;
			return Math.Abs(clockwise - target) < Math.Abs(antiClockwise - target);
		}
		// If there are no priorities, choose randomly
		var result = Random.Range(1, 3);
		return result == 1;
		
	}
}
