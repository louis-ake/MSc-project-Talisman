using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO19 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Tavern (Space O-19)" + "\n" + "\n" +
		           "Roll 1 die: 1-2) Fight enemy of strength " + UniqueTiles.GenericEnemy + ". 3) Gambled and lost 1 gold. 4-5) Gambled and won 1 gold. 6) Transported to temple.";
	}
}
