using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	// Each players starting stats - currently the same for all
	public int lives = 3;
	public int strength = 4;
	public int craft = 4;
	public int darkFate = 2;
	public int lightFate = 2;

	public Tile startTile;
	public Tile endTile;

	public static Vector2 endPos;
	// number of seconds to complete move
	public float speed = 1f;

	public static void SetEndPos(Vector2 pos)
	{
		endPos = pos;
		Debug.Log(endPos);
	}

}
