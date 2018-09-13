using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM2 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Portal of Power (Space M-2)" + "\n" + "\n" +
		           "Pay a penalty: If you have more lives than gold pay 1 life otherwise pay 1 gold.";
	}
}
