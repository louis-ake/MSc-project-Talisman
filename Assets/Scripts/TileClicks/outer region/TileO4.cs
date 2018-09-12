using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO4 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Woods (Space O-4):" + "\n" + "\n" +
		           "Draw a card from the adventure deck.";
	}
}
