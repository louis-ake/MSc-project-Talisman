using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileO13 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "City (Space O-13)" + "\n" + "\n" +
		           "Can pay 2 gold to buy upgrade your armaments and raise your strength by 1.";
	}
}
