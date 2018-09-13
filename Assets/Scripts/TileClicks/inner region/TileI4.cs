using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI4 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Pits (Space I-4):" + "\n" + "\n" +
		           "Roll 1 die and fight that number of strength 4 pitfiends.";
	}
}
