using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI1 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Plain of Peril (Space I-1):" + "\n" + "\n" +
		           "Draw a card from the adventure deck. Add 2 to the strength of any enemies encountered here";
	}
}
