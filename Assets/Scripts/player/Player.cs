using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		// Initialise each player in the bottom-right Tile
		//playerTrans = TileO1.FindObjectOfType<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// public static Transform currentPosition = TileO1.FindObjectOfType<Transform>();

	// Each players starting stats - currently the same for all
	public int lives = 3;
	public int strength = 4;
	public int craft = 4;
	public int darkFate = 2;
	public int lightFate = 2;

	public Transform playerTrans;
	
	// For moving Player objects
	public static void move(Transform current, Transform next)
	{
		float step = Time.deltaTime; // standard speed
		current.transform.position = Vector2.MoveTowards(current.transform.position,
			next.transform.position, step);
	}

}
