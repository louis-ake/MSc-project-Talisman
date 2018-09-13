using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileI7 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Death (Space I-7):" + "\n" + "\n" +
		           "Fight death. Roll 2 dice for you a 2 dice for death.";
	}
}
