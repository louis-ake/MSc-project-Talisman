using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UniqueTiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Tiles which entail a fight
	public static readonly string[] FightTiles = {"O5"};

	
	/**
	 * If it's a fight tile, this method will choose the correct method and call it
	 */
	public static int ChooseFightTile(string tileName, int strength, int gold)
	{
		if (tileName == "O5")
		{
			return FightSentinal(strength);
		}
		else
		{
			return 0;
		}	
	}

	private static int FightSentinal(int PlayerStrength)
	{
		var sentinalStrength = 4; // to be changed
		var SentinalResult = sentinalStrength + Random.Range(1, 7);
		var playerResult = PlayerStrength + Random.Range(1, 7);
		var diff = playerResult - SentinalResult;
		if (diff > 0)
		{
			AdventureDeck._deckText = "You fought the sentinal and won (" + playerResult + " vs " + SentinalResult + ")";
			Player.done = true;
			Player.won = true;
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.MoveRegion("M", 16, "M4");
			}
			else
			{
				// same for yellowplayer
			}
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			AdventureDeck._deckText = "You fought the sentinal and lost (" + playerResult + " vs " + SentinalResult + ")";
			Player.won = false;
			GameControl.ReduceLives();
		}
		else
		{
			AdventureDeck._deckText = "You fought the sentinel and tied (" + playerResult + " vs " + SentinalResult + ")";
			Player.done = true;
			Player.won = true;
			GameControl.AlternateTurnTracker();
		}
		return diff;
	}
}
