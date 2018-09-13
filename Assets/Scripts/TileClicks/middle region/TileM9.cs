using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM9 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Warlock's Cave (Space M-9):" + "\n" + "\n" +
		           "Roll 1 die and complete the resulting quest and earn a talisman: 1-2) Lose a life. 3-4) Lose 1 strength. 5-6) Lose 1 gold.";
	}
}
