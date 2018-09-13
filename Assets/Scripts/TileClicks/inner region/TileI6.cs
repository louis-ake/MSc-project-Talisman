using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI6 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Warlock's Cave (Space I-6):" + "\n" + "\n" +
		           "Fight the Werewolf. To calculate the Werewolf's strength roll 3 dice (FO).";
	}
}
