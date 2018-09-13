using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileM16 : TileClick {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnMouseDown()
	{
		TileText = "Castle (Space M-16):" + "\n" + "\n" +
		           "Can pay the royal doctor to heal 1 life for 1 gold.";
	}
}
