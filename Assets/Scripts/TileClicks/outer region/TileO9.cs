using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO9 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Crags (Space O-9)" + "\n" + "\n" +
		           "Roll 1 die: 1-3) Fight an enemy of strength " + UniqueTiles.GenericEnemy + ". 4-5) Nothing. 6) Gain 1 strength.";
	}
}
