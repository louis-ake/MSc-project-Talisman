using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class GameControl : MonoBehaviour {
	
	// Used to manage and control the game

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameFinished)
		{
			Main();	
		}
	}

	public Transform target;

	public bool gameFinished = false;
	

	void Main() 
	{
		{
			GameObject o2 = GameObject.Find("O2");
			target = o2.transform;
			float tx = target.position.x;
			float ty = target.position.y;
			Debug.Log("x = " + tx);
			Debug.Log("y = " + ty);
			Player.SetEndPos(new Vector2(tx, ty));
			if (BluePlayer.currentPos == BluePlayer.endPos)
			{
				gameFinished = true;
			}
		}
	}
}
