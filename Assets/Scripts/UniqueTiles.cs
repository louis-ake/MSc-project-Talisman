using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

	public static readonly string[] LifeLossDraw = {"M10"};

	// Tiles which entail a fight
	public static readonly string[] FightTiles = {"O5", "I6"};


	private static void FightGenericEnemy()
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
			AdventureDeck._deckText = "fought strength " + GenericEnemy + " enemy and won";
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
		switch (tileName)
		{
			case "O5":
				return FightSentinal();
			case "I6":
				return FightWarlock();
			default:
				return 0;
		}
	}


	private static int FightWarlock()
	{
		// Two to determine its strength, one to determine its fight roll
		var warlockResult = Random.Range(1, 7) + Random.Range(1, 7) +Random.Range(1, 7);
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		var diff = playerResult - warlockResult;
		if (diff > 0)
		{
			AdventureDeck._deckText = "You fought the warlock and won (" + playerResult + " vs " + warlockResult + ")";
			Player.done = true;
			Player.won = true;
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			AdventureDeck._deckText = "You fought the warlock and lost (" + playerResult + " vs " + warlockResult + ")";
			Player.won = false;
			GameControl.ChangeLives(-1);
		}
		else
		{
			AdventureDeck._deckText = "You fought the warlock and tied (" + playerResult + " vs " + warlockResult + ")";
			Player.won = true;
			Player.done = true;
			GameControl.AlternateTurnTracker();
		}

		return diff;
	}

	/**
	 * Fight the sentinal and if won, cross into the middle region
	 */
	private static int FightSentinal()
	{
		var sentinalStrength = 8; // to be changed
		var SentinalResult = sentinalStrength + Random.Range(1, 7);
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
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
				YellowPlayer.MoveRegion("M", 16, "M4");
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


	public static readonly string[] Tiles = {"O1", "O3", "O7", "O9", "O19", "O23", "M1", "M2", "M9", "M13", 
		"I2", "I3", "I4", "I5", "I7", "I8", "C1"};
	

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
			case "I2":
			case "I8":
				MinesCrypt();
				break;
			case "I3":
				VampiresTower();
				break;
			case "I4":
				PitFiends();
				break;
			case "I5":
				ValleyOfFire();
				break;
			case "I7":
				Death();
				break;
			case "C1":
				Crown();
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
				BluePlayer.MoveRegion("M", 16, "M13");
			}
			else
			{
				YellowPlayer.MoveRegion("M", 16, "M13");
			}	
			AdventureDeck._deckText = "You rolled a 6 and were transported to the temple";
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
				YellowPlayer.MoveRegion("I", 8, "I1");
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

	private static void MinesCrypt()
	{
		var result = Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7) - GameControl.GetStrength();
		if (GameControl.TurnTracker == 0)
		{
			if (result == 1)
			{
				BluePlayer.MoveRegion("I", 8, "I1");
				AdventureDeck._deckText = "result of 1: transported to plain of peril";
			} else if (result >= 2 && result <= 3)
			{
				BluePlayer.MoveRegion("M", 16, "M1");
				AdventureDeck._deckText = "result of 2-3: transported to the portal of power";
			} else if (result >= 4 && result <= 5)
			{
				BluePlayer.MoveRegion("M", 16, "M9");
				AdventureDeck._deckText = "result of 4-5: transported to the warlock's cave";
			} else if (result >= 6)
			{
				BluePlayer.MoveRegion("O", 24, "O19");
				AdventureDeck._deckText = "result of 6+: transported to the tavern";
			}
		}
		else
		{
			if (result == 1)
			{
				YellowPlayer.MoveRegion("I", 8, "I1");
				AdventureDeck._deckText = "result of 1: transported to plain of peril";
			} else if (result >= 2 && result <= 3)
			{
				YellowPlayer.MoveRegion("M", 16, "M1");
				AdventureDeck._deckText = "result of 2-3: transported to the portal of power";
			} else if (result >= 4 && result <= 5)
			{
				YellowPlayer.MoveRegion("M", 16, "M9");
				AdventureDeck._deckText = "result of 4-5: transported to the warlock's cave";
			} else if (result >= 6)
			{
				YellowPlayer.MoveRegion("O", 24, "O19");
				AdventureDeck._deckText = "result of 6+: transported to the tavern";
			}
		}
	}


	private static void VampiresTower()
	{
		var result = Random.Range(1, 7);
		if (result <= 2)
		{
			AdventureDeck._deckText = "rolled 1-2 so no effect";
		} else if (result >= 3 && result <= 4)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "rolled 3-4 and lost a life";
		} 
		else
		{
			GameControl.ChangeLives(-2);
			AdventureDeck._deckText = "rolled 5-6 and lost 2 lives";
		}
	}
	

	private static void PitFiends()
	{
		const int fiendStrength = 4;
		var fiends = Random.Range(1, 7);
		var lost = 0;
		for (var i = 0; i < fiends; i++)
		{
			var fiendResult = fiendStrength + Random.Range(1, 7);
			var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
			if (fiendResult <= playerResult) continue;
			GameControl.ChangeLives(-1);
			lost += 1;
		}
		AdventureDeck._deckText = "You fought " + fiends + " fiends and defeated " + (fiends - lost);
	}


	private static void ValleyOfFire()
	{
		if (!GameControl.CheckTalisman()) return;
		if (GameControl.TurnTracker == 0)
		{
			BluePlayer.MoveRegion("C", 1, "C1");
		}
		else
		{
			YellowPlayer.MoveRegion("C", 1, "C1");
		}
	}


	private static void Death()
	{
		var deathresult = Random.Range(1, 7) + Random.Range(1, 7);
		var playerResult = Random.Range(1, 7) + Random.Range(1, 7);
		if (deathresult > playerResult)
		{
			GameControl.ChangeLives(-1);
			AdventureDeck._deckText = "you diced with death and lost (" + deathresult + " vs " + playerResult + ")";
		}
		else
		{
			AdventureDeck._deckText = "you diced with death and survived (" + deathresult + " vs " + playerResult + ")";
		}
	}


	private static void Crown()
	{
		GameControl.EndGame();
	}
	
}
