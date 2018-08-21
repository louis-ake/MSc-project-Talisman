using System.Collections;
using System.Collections.Generic;
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

	// Tiles on which to draw from the deck
	public static readonly string[] CardTiles =
		{"O2", "O4", "O6", "O8", "O10", "O11", "O12", "O14", "O15", "O16", "O17", "O18", "O20", "O22", "O24"};
	

	public static int FightBandit(int PlayerStrength)
	{
		var enemyStrength = 4;
		var banditResult = enemyStrength + Random.Range(1, 7);
		var playerResult = PlayerStrength + Random.Range(1, 7);
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
