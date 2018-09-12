using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO7 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Chapel (Space O-7)" + "\n" + "\n" +
		           "If good replenish fate; if evil lose a life.";
	}
}
