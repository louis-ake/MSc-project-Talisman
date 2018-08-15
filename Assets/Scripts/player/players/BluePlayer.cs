using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : Player {

	// Use this for initialization
	void Start ()
	{
		// Initialise each player in the bottom-right Tile
		this.transform.position = new Vector2(15, -15);
		CurrentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// this must be updated every frame for algorithm to work
		if (!Active) return;
		CurrentPos = this.transform.position;
		if (Vector2.Distance(new Vector2(CurrentPos.x, CurrentPos.y), EndPos) > 0)
		{
			this.transform.position = Vector2.MoveTowards(CurrentPos, EndPos, Speed);
		}
	}

	public static bool Active = false;

	public static bool BluePlayerMoveCalculated = false;
}
