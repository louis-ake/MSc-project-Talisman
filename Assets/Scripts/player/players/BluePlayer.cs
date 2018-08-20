using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using Random = System.Random;

public class BluePlayer : Player {
	
	/**
	 * BluePlayer and YellowPlayer mirror each other appart from starting transform,
	 * StartTileName string and turnTracker value.
	 */

	// Use this for initialization
	void Start ()
	{
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(15, -15);
		_currentPos = this.transform.position;
		SetStats();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// this must be updated every frame for algorithm to work
		if (!_active) return;
		_currentPos = this.transform.position;
		if (Vector2.Distance(new Vector2(_currentPos.x, _currentPos.y), _endPos) > 0)
		{
			this.transform.position = Vector2.MoveTowards(_currentPos, _endPos, Speed);
		}
		SetStats();
	}
	
	// Each players starting stats - currently the same for all
	public static int lives = 3;
	public static int strength = 4;
	public static int strengthTrophy = 0;
	public static int craft = 4;
	public static int craftTrophy = 0;
	public static int fateTokens = 2;
	public static int gold = 4;

	private static string _startTileName = "O1";
	
	// So that movement works correctly in different regions
	public static string Region = "O"; // O/M/I/C
	public static int RegionUpperBound = 24; // 24/16/8/1
	
	public static int Turns = 0;

	private static Vector2 _endPos;
	private static Vector2 _currentPos;

	private static bool _active = false;

	private static Transform _target;
	
	// flags for a turns's control flow
	private static bool moved;
	private static bool actionNeeded = true; 

	public Text Stats;

	private void SetStats()
	{
		Stats.text = "Blue Player's Stats" + "\n"+ "\n" + 
		             "lives: " + lives + "\n" + 
		             "strength: " + strength + " (" + strengthTrophy + ")" + "\n" + 
		             "craft: " + craft + " (" + craftTrophy + ")" + "\n" +
		             "fate tokens: " + fateTokens + "\n" + 
		             "gold: " + gold + "\n" + "\n" + 
		             "turns: " + Turns;
	}
	

	private static void SetEndPos(Vector2 pos)
	{
		_endPos = pos;
	}


	private static void SetStartTileName(string name)
	{
		_startTileName = name;
	}


	public static void TakeTurn()
	{
		if (won == true)
		{
			Move();
		}
		if (AdventureDeck.CardTiles.Contains(_startTileName))
		{
			DrawFromDeck();
		} else if (UniqueTiles.FightTiles.Contains(_startTileName))
		{
			EncounterUniqueFightTile();
		} else if (UniqueTiles.NonFightTiles.Contains(_startTileName) && moved && actionNeeded)
		{
			UniqueTiles.ChooseNonFightTile(_startTileName);
			actionNeeded = false;
			GameControl.AlternateTurnTracker();
		} else if (moved && actionNeeded)
		{
			actionNeeded = false;
			GameControl.AlternateTurnTracker();
		}
		
	}

	private static void EncounterUniqueFightTile()
	{
		if (moved == true && done == false && actionNeeded == true)
		{
			FightDiff = UniqueTiles.ChooseFightTile(_startTileName, strength, gold);
			actionNeeded = false;
		}
		if (won == false && done == false && moved == true)
		{
			UseFate(FightDiff);
		} else if (won == true && moved == true && done == true)
		{
			GameControl.TurnTracker = 1;
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
		if (moved == true && done == false && actionNeeded == true)
		{
			FightDiff = AdventureDeck.FightBandit(strength);
			actionNeeded = false;
		}
		if (won == false && done == false && moved == true)
		{
			UseFate(FightDiff);
		} else if (won == true && moved == true && done == true)
		{
			GameControl.TurnTracker = 1;
			done = false;
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
			var challenge = UnityEngine.Random.Range(1, 7);
			var result = challenge - diff;
			if (result >= 0)
			{
				lives += 1;
				Decision = "Successful! (rolled = " + challenge + ")";
			}
			else
			{
				Decision = "Unsuccessful (rolled = " + challenge + ")";
			}
			won = true;
			fateTokens -= 1;
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
		var nextTileNo = 0;
		// GameControl.DirectionDecision.text = "Press c to move clockwise or v to move anticlockwise";
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

}
