using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM10 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Desert (Space M-10):" + "\n" + "\n" +
		           "Draw a card from the adventure deck.";
	}
}
