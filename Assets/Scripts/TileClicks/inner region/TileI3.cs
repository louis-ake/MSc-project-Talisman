using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI3 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Vampire's Tower (Space I-3):" + "\n" + "\n" +
		           "Roll 1 die: 1-2) No effect. 3-4) Lose a life. 5-6) Lose 2 lives.";
	}
}
