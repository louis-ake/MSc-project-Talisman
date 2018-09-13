using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI8 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Crypt (Space I-8):" + "\n" + "\n" +
		           "Roll 3 dice and subtract your strength: 1) Transported to Plain of Peril. 2-3) Transported to Portal of Power. 4-5) Transported to Warlock's Cave. 6+) Transported to Tavern.";
	}
}
