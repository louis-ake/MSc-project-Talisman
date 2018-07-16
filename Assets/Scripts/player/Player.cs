using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Transform currentPosition;

	// Each players starting stats - currently the same for all
	public int lives = 3;
	public int strength = 4;
	public int craft = 4;
	public int darkFate = 2;
	public int lightFate = 2;
	
	
	// For moving Player objects
	public void move(Tile t)
	{
		float step = Time.deltaTime;
		Transform newPosition = t.transform;
		Vector2.MoveTowards(currentPosition.transform.position,
			newPosition.transform.position, step);
	}

}
