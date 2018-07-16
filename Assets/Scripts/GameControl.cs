using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		main();
	}

	public bool gameFinished = false;

	void main() 
	{
		while (!gameFinished)
		{
			GameObject secondG = GameObject.Find("TileO2");
			Transform second = secondG.transform;
			Transform current = this.transform;
			Player.move(current, second);
			
		}
	}
}
