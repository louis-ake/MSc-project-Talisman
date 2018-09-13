using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM3 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Hidden Valley (Space M-3):" + "\n" + "\n" +
		           "Draw a card from the adventure deck. Add 2 to the strength of any enemies encountered.";
	}
}
