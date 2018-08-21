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
	
	private static readonly int GenericEnemy = 3;

	// Tiles which entail a fight
	public static readonly string[] FightTiles = {"O5"};


	public static void FightGenericEnemy()
	{
		var enemyResult = GenericEnemy + Random.Range(1, 7);
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		if (enemyResult > playerResult)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "fought strength " + GenericEnemy + " enemy and lost";
		} else if (playerResult > enemyResult)
		{
			GameControl.ChangeStrengthTrophy(GenericEnemy);
			AdventureDeck._deckText = "fought strength " + GenericEnemy + " enemy and lost";
		}
		else
		{
			AdventureDeck._deckText = "fought strength " + GenericEnemy + " enemy and tied";
		}
	}

	
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

	/**
	 * Fight the sentinal and if won, cross into the middle region
	 */
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
			GameControl.ChangeLives(-1);
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

	public static readonly string[] ArmouryTiles = {"O13"};

	public static readonly int ArmouryPrice = 2;


	public static readonly string[] Tiles = {"O1", "O3", "O7", "O9", "O23"};
	

	public static void ChooseTile(string tileName)
	{
		switch (tileName)
		{
			case "O1":
				Village();
				break;
			case "O3":
				Graveyard();
				break;
			case "O7":
				Chapel();
				break;
			case "O9":
			case "O23":
				CragsForest();
				break;
		}
	}
	
	/**
	 * Roll a 6-sided die and incur effect accordingly
	 */
	private static void Village()
	{
		var result = Random.Range(1, 7);
		if (result == 1)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "Rolled 1 and lost 1 life";
		} else if (result >= 2 && result <= 3)
		{
			GameControl.ChangeStrength(-1);
			AdventureDeck._deckText = "Rolled 2-3 and lost 1 strength";
		} else if (result >= 4 && result <= 5)
		{
			GameControl.ChangeStrength(1);
			AdventureDeck._deckText = "Rolled 4-5 and gained 1 life";
		} else
		{
			GameControl.ChangeStrength(1);
			AdventureDeck._deckText = "Rolled 6 and gained a life";
		}
	}


	private static void Graveyard()
	{
		if (GameControl.TurnTracker == 0)
		{
			if (BluePlayer.alignment == "good")
			{
				GameControl.ChangeLives(-1);
			}
			else
			{
				GameControl.ReplenishFate();
			}
		}
		else
		{
			if (YellowPlayer.alignment == "good")
			{
				GameControl.ChangeLives(-1);
			}
			else
			{
				GameControl.ReplenishFate();
			}
		}
	}


	private static void Chapel()
	{
		if (GameControl.TurnTracker == 0)
		{
			if (BluePlayer.alignment == "evil")
			{
				GameControl.ChangeLives(-1);
			}
			else
			{
				GameControl.ReplenishFate();
			}
		}
		else
		{
			if (YellowPlayer.alignment == "evil")
			{
				GameControl.ChangeLives(-1);
			}
			else
			{
				GameControl.ReplenishFate();
			}
		}
	}


	public static void CragsForest()
	{
		var result = Random.Range(1, 7);
		if (result <= 3)
		{
			FightGenericEnemy();
		} else if (result >= 4 && result <= 5)
		{
			AdventureDeck._deckText = "Rolled 4-5 and nothing happened";
		}
		else
		{
			GameControl.ChangeStrength(1);
			AdventureDeck._deckText = "Rolled a 6 and gained 1 strength";
		}
	}

}
