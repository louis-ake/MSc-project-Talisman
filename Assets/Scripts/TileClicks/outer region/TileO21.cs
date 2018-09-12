using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO21 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Ruins (Space O-21)" + "\n" + "\n" +
		           "Draw a card from the adventure deck.";
	}
}
