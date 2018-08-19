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
	private static string _deckText = "";

	public static int FightBandit(int PlayerStrength)
	{
		var enemyStrength = 4;
		var banditResult = enemyStrength + Random.Range(1, 7);
		var playerResult = PlayerStrength + Random.Range(1, 7);
		var diff = playerResult - banditResult;
		if (diff > 0)
		{
			_deckText = "You fought a bandit of strength 4 and won (" + playerResult + " vs " + banditResult + ")";
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.strengthTrophy += enemyStrength;
				GameControl.TurnTracker = 1;
			}
			else
			{
				YellowPlayer.strengthTrophy += enemyStrength;
				GameControl.TurnTracker = 0;
			}

			Player.done = true;
			Player.won = true;
		} else if (diff < 0)
		{
			_deckText = "You fought a bandit of strength 4 and lost (" + playerResult + " vs " + banditResult + ")";
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.lives -= 1;
			}
			else
			{
				YellowPlayer.lives -= 1;
			}
			Player.won = false;
		}
		else
		{
			_deckText = "You fought a bandit of strength 4 and tied (" + playerResult + " vs " + banditResult + ")";
			Player.done = true;
			Player.won = true;
			if (GameControl.TurnTracker == 0)
			{
				GameControl.TurnTracker = 1;
			}
			else
			{
				GameControl.TurnTracker = 0;
			}
		}

		return diff;
	}
}
