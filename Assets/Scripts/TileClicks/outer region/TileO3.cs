using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO3 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Graveyard (Space O-3)" + "\n" + "\n" +
		           "If evil replenish fate; if good lose a life.";
	}
}
