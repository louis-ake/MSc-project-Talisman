using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI5 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Valley of Fire (Space I-5):" + "\n" + "\n" +
		           "If you have a Talisman, you are transported to the crown of command.";
	}
}
