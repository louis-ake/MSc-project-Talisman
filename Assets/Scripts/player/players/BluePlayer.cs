using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : Player {

	// Use this for initialization
	void Start ()
	{
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(15, -15);
		currentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// this must be updated every frame for algorithm to work
		currentPos = this.transform.position;
		if (Vector2.Distance(new Vector2(currentPos.x, currentPos.y), endPos) > 0)
		{
			this.transform.position = Vector2.MoveTowards(currentPos, endPos, speed);
		}
	}
	public static Vector2 currentPos;
}
