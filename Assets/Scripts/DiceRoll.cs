using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DiceValues = new int[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Results will be stored in public values so they can be accessed from elsewhere
    public int[] DiceValues;
    public int DiceTotal;

    public void Roll() {
        // Will need to be able variable numbers of dice
        DiceTotal = 0;
        for (int i = 0; i < DiceValues.Length; i++) {
            int dieResult = Random.Range(0, 7);
            DiceTotal += dieResult;
            DiceValues[i] = dieResult;
        }
        Debug.Log("Rolled: " + DiceTotal);
    }
}
