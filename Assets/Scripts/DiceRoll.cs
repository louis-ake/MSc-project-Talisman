using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRoll : MonoBehaviour {


	// Use this for initialization
	void Start () {
        DiceValues = new int[1];
		SetResultText();
	}
	
	// Update is called once per frame
	void Update () {
		SetResultText();
	}

    // Results will be stored in public values so they can be accessed from elsewhere
    public int[] DiceValues;
    public static int DiceTotal;
	public Text ResultText;

	// Will be compared with turncount to check desired equivalence
	public static int RollCount = 0;

    // Array of sprites for die face sprites
    public Sprite[] DieFaces;

    public void Roll() {
        // Will need to be able to roll variable numbers of dice
	    if (RollCount > GameControl.TurnCount) { return; }
	    Player.Decision = "";
	    GameControl.SetTurnText();
        DiceTotal = 0;
        for (var i = 0; i < DiceValues.Length; i++) {
            var dieResult = Random.Range(1, 7);
            DiceTotal += dieResult;
            DiceValues[i] = dieResult;
            // Sets the image to the one corresponding to the die roll
            this.transform.GetChild(i).GetComponent<Image>().sprite = 
                DieFaces[dieResult - 1];
        }
		RollCount += 1;
    }

	void SetResultText()
	// To keep the text variable updated with the most recent dice roll
	{
		String s = "";
		for (var i = 0; i < DiceValues.Length; i++)
		{
			s = s + DiceValues[i];
		}
		ResultText.text = "Rolled " + s + "\n" + "\n" + "Move clockwise = 'C'	Anticlockwise = 'V'";
	}
}