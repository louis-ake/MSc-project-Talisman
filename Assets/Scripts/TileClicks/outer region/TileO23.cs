using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO23 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Forest (Space O-23)" + "\n" + "\n" +
		           "Roll 1 die: 1-3) Fight an enemy of strength " + UniqueTiles.GenericEnemy + ". 4-5) Nothing. 6) Gain 1 strength.";
	}
}
