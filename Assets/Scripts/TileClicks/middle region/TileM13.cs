using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM13 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Temple (Space M-13):" + "\n" + "\n" +
		           "Roll 2 dice: 2) Lose 2 lives. 3-5) Lose 1 life. 6-8) Gain 1 strength. 9-10) Gain a Talisman. 11) Replenish fate. 12) Gain 2 lives.";
	}
}
