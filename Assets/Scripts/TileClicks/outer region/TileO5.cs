using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO5 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Sentinal (Space O-5)" + "\n" + "\n" +
		           "Fight the sentinal of strength 8. If you win, cross over into the middle region (FO).";
	}
}
