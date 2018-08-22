using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class AdventureDeck : MonoBehaviour {

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
	

	// Workaround to get text to show on screen (must be a 'public Text' to show in inspector)
	public Text DeckText;
	public static string _deckText = "";

	public static readonly string[] AllCardTiles = {"O2", "O4", "O6", "O8", "O10", "O11", "O12", "O14", "O15", "O16", 
		"O17", "O18", "O20", "O22", "O24", "M5", "M6", "M7", "M8", "M15"} ;

	public static readonly string[] BonusTiles = {"M5", "M6", "M7", "M8", "M15"};
	

	// Tiles on which to draw from the deck
	public static readonly string[] CardTiles =
		{"O2", "O4", "O6", "O8", "O10", "O11", "O12", "O14", "O15", "O16", "O17", "O18", "O20", "O22", "O24"};


	public static int ProduceCard(string tileName)
	{
		if (CardTiles.Contains(tileName))
		{
			return FightBandit(0);
		} 
		else
		{
			return FightBandit(2);
		}
	}


	private static int FightBandit(int bonus)
	{
		var enemyStrength = 4;
		var banditResult = enemyStrength + Random.Range(1, 7) + bonus;
		var playerResult = GameControl.GetStrength() + Random.Range(1, 7);
		var diff = playerResult - banditResult;
		if (diff > 0)
		{
			_deckText = "You fought a bandit of strength 4 and won (" + playerResult + " vs " + banditResult + ")";
			GameControl.ChangeStrengthTrophy(enemyStrength);
			Player.done = true;
			Player.won = true;
			GameControl.AlternateTurnTracker();
		} else if (diff < 0)
		{
			_deckText = "You fought a bandit of strength 4 and lost (" + playerResult + " vs " + banditResult + ")";
			GameControl.ChangeLives(-1);
			Player.won = false;
		}
		else
		{
			_deckText = "You fought a bandit of strength 4 and tied (" + playerResult + " vs " + banditResult + ")";
			Player.done = true;
			Player.won = true;
			GameControl.AlternateTurnTracker();
		}
		return diff;
	}
}
