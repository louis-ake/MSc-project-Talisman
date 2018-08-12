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
    public int DiceTotal;
	public Text ResultText;

    // Array of sprites for die face sprites
    public Sprite[] dieFaces;

    public void Roll() {
        // Will need to be able to roll variable numbers of dice
        DiceTotal = 0;
        for (int i = 0; i < DiceValues.Length; i++) {
            int dieResult = Random.Range(1, 7);
            DiceTotal += dieResult;
            DiceValues[i] = dieResult;
            // Sets the image to the one corresponding to the die roll
            this.transform.GetChild(i).GetComponent<Image>().sprite = 
                dieFaces[dieResult - 1];
        }
        Debug.Log("Rolled: " + DiceTotal);
    }

	public void SetResultText()
	{
		String s = "";
		for (int i = 0; i < DiceValues.Length; i++)
		{
			s = s + DiceValues[i].ToString();
		}
		ResultText.text = "Rolled " + s;
	}
}