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

	public Text DeckText;

	private static string _deckText = "";

	//private static Text _deckText;

	/*private static void Set_deckText(string s)
	{
		_deckText.text = s;
	}*/
	
	public static void FightBandit(int PlayerStrength)
	{
		var enemyStrength = 4;
		var banditResult = enemyStrength + Random.Range(1, 7);
		var playerResult = PlayerStrength + Random.Range(1, 7);
		if (playerResult > banditResult)
		{
			_deckText = "You fought a bandit of strength 4 and won";
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.strengthTrophy += enemyStrength;
			}
			else
			{
				YellowPlayer.strengthTrophy += enemyStrength;
			}
		} else if (banditResult > playerResult)
		{
			_deckText = "You fought a bandit of strength 4 and lost";
			if (GameControl.TurnTracker == 0)
			{
				BluePlayer.lives -= 1;
			}
			else
			{
				YellowPlayer.lives -= 1;
			}
		}
		else
		{
			_deckText = "You fought a bandit of strength 4 and tied";
		}
	}
}
