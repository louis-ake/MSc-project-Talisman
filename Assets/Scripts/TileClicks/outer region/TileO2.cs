﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO2 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Fields (Space O-2)" + "\n" + "\n" +
		           "Draw a card from the adventure deck.";
	}
}
