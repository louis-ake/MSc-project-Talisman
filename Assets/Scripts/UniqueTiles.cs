using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UniqueTiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static readonly int ArmouryPrice = 2;
	
	public static readonly string[] ArmouryTiles = {"O13"};

	public static readonly int HealPrice = 1;

	public static readonly string[] HealTiles = {"M16"};
	
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
	public static int ChooseFightTile(string tileName)
	{
		if (tileName == "O5")
		{
			return FightSentinal(GameControl.GetStrength());
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
			AdventureDeck._deckText =
				"You fought the sentinal and won (" + playerResult + " vs " + SentinalResult + ")";
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
		}
		else if (diff < 0)
		{
			AdventureDeck._deckText =
				"You fought the sentinal and lost (" + playerResult + " vs " + SentinalResult + ")";
			Player.won = false;
			GameControl.ChangeLives(-1);
		}
		else
		{
			AdventureDeck._deckText =
				"You fought the sentinel and tied (" + playerResult + " vs " + SentinalResult + ")";
			Player.done = true;
			Player.won = true;
			GameControl.AlternateTurnTracker();
		}

		return diff;
	}


	public static readonly string[] Tiles = {"O1", "O3", "O7", "O9", "O23", "M1", "M2", "M9", "M13"};
	

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
			case "O19":
				Tavern();
				break;
			case "M1":
				PortalOfPower();
				break;
			case "M2":
				BlackKnight();
				break;
			case "M9":
				WarlockCave();
				break;
			case "M13":
				Temple();
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


	private static void CragsForest()
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


	private static void Tavern()
	{
		var result = Random.Range(1, 7);
		if (result <= 2)
		{
			FightGenericEnemy();
		} else if (result == 3)
		{
			GameControl.ChangeGold(-1);
			AdventureDeck._deckText = "You gambled and lost 1 gold";
		} else if (result >= 4 && result <= 5)
		{
			GameControl.ChangeGold(1);
			AdventureDeck._deckText = "You gambled and gained 1 gold";
		}
		else
		{
			if (GameControl.TurnTracker == 0)
			{
				AdventureDeck._deckText = "You rolled a 6 and were transported to the temple";
				BluePlayer.MoveRegion("M", 16, "M13");
			}
			else
			{
				// same for yellowplayer
			}	
		}
	}


	private static void PortalOfPower()
	{
		if (Random.Range(1, 7) + Random.Range(1, 7) <= GameControl.GetStrength())
		{
			AdventureDeck._deckText = "successful roll - transported to Plain of Peril";
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.MoveRegion("I", 8, "I1");
			}
			else
			{
				// same for yellowplayer
			}
		}
		else
		{
			AdventureDeck._deckText = "Unsuccessful roll";
		}
	}


	private static void BlackKnight()
	{
		if (GameControl.GetGold() >= GameControl.GetLives())
		{
			GameControl.ChangeGold(-1);
		}
		else
		{
			GameControl.ChangeLives(-1);
		}
		AdventureDeck._deckText = "penalty paid";
	}


	private static void Temple()
	{
		var result = Random.Range(1, 7) + Random.Range(1, 7);
		if (result == 2)
		{
			GameControl.ChangeLives(-2);
			AdventureDeck._deckText = "rolled 2 and lost 2 lives";
		} else if (result >= 3 && result <= 5)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "rolled 3-5 and lost a life";
		} else if (result >= 6 && result <= 8)
		{
			GameControl.ChangeStrength(1);
			AdventureDeck._deckText = "rolled 6-8 and gained 1 strength";
		} else if (result >= 9 && result <= 10)
		{
			GameControl.GiveTalisman();
			AdventureDeck._deckText = "rolled 9-10 and gained a talisman";
		} else if (result == 11)
		{
			GameControl.ReplenishFate();
			AdventureDeck._deckText = "rolled an 11 and fate was replenished";
		}
		else
		{
			GameControl.ChangeLives(2);
			AdventureDeck._deckText = "rolled a 12 and gained 2 lives";
		}
	}
	

	private static void WarlockCave()
	{
		var result = Random.Range(1, 7);
		if (result <= 2)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "rolled 1-2 and lost a life to complete quest";
		} else if (result >= 3 && result <= 4)
		{
			if (GameControl.GetStrength() < 1)
			{
				return;
			}
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "rolled 3-4 and lost 1 strength to complete quest";

		}
		else
		{
			if (GameControl.GetGold() < 1)
			{
				return;
			}
			GameControl.ChangeGold(-1);
			AdventureDeck._deckText = "rolled 5-6 and lost 1 gold to complete quest";
		}
		GameControl.GiveTalisman();
	}
}
