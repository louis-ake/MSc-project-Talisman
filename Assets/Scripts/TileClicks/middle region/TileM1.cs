using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM1 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Portal of Power (Space M-1)" + "\n" + "\n" +
		           "Roll 2 dice: if the result is less than or equal to your strength you are transported to the plain of peril.";
	}
}
