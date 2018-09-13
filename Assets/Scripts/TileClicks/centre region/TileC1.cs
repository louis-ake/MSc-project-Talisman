using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileC1 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Crown of Command (Space C-1):" + "\n" + "\n" +
		           "Make it here and you win!";
	}
}
