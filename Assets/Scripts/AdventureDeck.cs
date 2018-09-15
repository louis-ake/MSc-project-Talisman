using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class AdventureDeck : MonoBehaviour {
	
	/**
	 * This class represents the adventure deck
	 */

	// Use this for initialization
	void Start ()
	{
		DeckText.text = _deckText;
	}
	
	// Update is called once per frame
	void Update ()
	{
		DeckText.text = _deckText;
	}
	

	// To display effects result from cards drawn. Also used by UniqueTiles class.
	public Text DeckText;
	public static string _deckText = "";
	
	// Tiles on which to draw from the deck
	public static readonly string[] AllCardTiles = {"O2", "O4", "O6", "O8", "O10", "O11", "O12", "O14", "O15", "O16", 
		"O17", "O18", "O20", "O21", "O22", "O24", "M3", "M4", "M5", "M6", "M7", "M8", "M10", "M11", "M12", "M14", "M15", "I1"} ;
	
	// those spaces which don't add a bonus to enemy strength
	private static readonly string[] CardTiles =
		{"O2", "O4", "O6", "O8", "O10", "O11", "O12", "O14", "O15", "O16", "O17", "O18", "O20", "O21", "O22", "O24", "M4", "M10", "M12", "M14"};

	// those that do add a bonus to enemy strength
	public static readonly string[] BonusTiles = {"M3", "M5", "M6", "M7", "M8", "M11", "M15", "I1"};
	// the bonus added to eneemy strength on certain spaces
	private const int Bonus = 2;

	/**
	 * Generates a random number between 1 and 8. Calls method (card)
	 * according to result. Cards weighted.
	 */
	public static int ProduceCard(string tileName)
	{
		var decideCard = Random.Range(1, 9);
		if (decideCard <= 3)
		{
			return FightBandit(CardTiles.Contains(tileName) ? 0 : Bonus);
		} 
		if (decideCard >= 4 && decideCard <= 5)
		{
			return FightOgre(CardTiles.Contains(tileName) ? 0 : Bonus);
		} 
		if (decideCard == 6)
		{
			return FightDragon(CardTiles.Contains(tileName) ? 0 : Bonus);
		} 
		if (decideCard == 7)
		{
			BagOfGold();
			return 0;
		}	
		Talisman();
		return 0;
	}


	/**
	 * Enemy has a set strength. A die is rolled for the enemy and one for the player
	 * and these are added to their strength to calculate fight results. If player
	 * result is higher their strenght trophy is raised by the enemy strength and the
	 * turn ends. If they lose, their lives are decremented. If they tie the turn ends.
	 * Difference between fight results is returns for use with fate system.
	 */
	private static int FightBandit(int bonus)
	{
		var enemyStrength = 4;
		var enemyResult = enemyStrength + Random.Range(1, 7) + bonus;
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		var diff = playerResult - enemyResult;
		if (diff > 0)
		{
			_deckText = "You fought a bandit of strength 4 and won (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeStrengthTrophy(enemyStrength);
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			_deckText = "You fought a bandit of strength 4 and lost (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeLives(-1);
			Player.Done = false;
		}
		else
		{
			_deckText = "You fought a bandit of strength 4 and tied (" + playerResult + " vs " + enemyResult + ")";
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		}
		return diff;
	}


	/**
	 * Enemy has a set strength. A die is rolled for the enemy and one for the player
	 * and these are added to their strength to calculate fight results. If player
	 * result is higher their strenght trophy is raised by the enemy strength and the
	 * turn ends. If they lose, their lives are decremented. If they tie the turn ends.
	 * Difference between fight results is returns for use with fate system.
	 */
	private static int FightOgre(int bonus)
	{
		var enemyStrength = 5;
		var enemyResult = enemyStrength + Random.Range(1, 7) + bonus;
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		var diff = playerResult - enemyResult;
		if (diff > 0)
		{
			_deckText = "You fought an ogre of strength 5 and won (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeStrengthTrophy(enemyStrength);
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			_deckText = "You fought an ogre of strength 5 and lost (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeLives(-1);
			Player.Done = false;
		}
		else
		{
			_deckText = "You fought an ogre of strength 5 and tied (" + playerResult + " vs " + enemyResult + ")";
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		}
		return diff;
	}


	/**
	 * Enemy has a set strength. A die is rolled for the enemy and one for the player
	 * and these are added to their strength to calculate fight results. If player
	 * result is higher their strenght trophy is raised by the enemy strength and the
	 * turn ends. If they lose, their lives are decremented. If they tie the turn ends.
	 * Difference between fight results is returns for use with fate system.
	 */
	private static int FightDragon(int bonus)
	{
		var enemyStrength = 6;
		var enemyResult = enemyStrength + Random.Range(1, 7) + bonus;
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		var diff = playerResult - enemyResult;
		if (diff > 0)
		{
			_deckText = "You fought a dragon of strength 6 and won (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeStrengthTrophy(enemyStrength);
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			_deckText = "You fought a dragon of strength 6 and lost (" + playerResult + " vs " + enemyResult + ")";
			GameControl.ChangeLives(-1);
			Player.Done = false;
		}
		else
		{
			_deckText = "You fought a dragon of strength 6 and tied (" + playerResult + " vs " + enemyResult + ")";
			Player.Done = true;
			GameControl.AlternateTurnTracker();
		}
		return diff;
	}

	private static void BagOfGold()
	{
		GameControl.ChangeGold(1);
		_deckText = "You found a bag of gold!";
		GameControl.AlternateTurnTracker();
	}


	private static void Talisman()
	{
		GameControl.GiveTalisman();
		_deckText = "You found a talisman!";
		GameControl.AlternateTurnTracker();
	}
}
