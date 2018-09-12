using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO1 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseDown()
	{
		TileText = "Village (Space O1)" + "\n" + "\n" +
		           "Roll 1 die: 1) Lose a life. 2-3) Lose 1 strength. 4-5) Gain 1 life. 6) Gain a life";
	}
}
