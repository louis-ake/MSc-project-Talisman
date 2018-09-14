using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		DecisionText.text = Decision;
	}
	
	// Update is called once per frame
	void Update()
	{
		DecisionText.text = Decision;
	}
	
	// flags for turn control flow
	public static bool Done = true;
	protected static bool Moved;
	protected static bool ActionNeeded;

	// used to initialise player statc
	public const int StartingLives = 4;
	public const int StartingStrength = 4;
	public const int StatingFateTokens = 3;
	public const int StartingGold = 3;

	// For displaying player decisions
	public Text DecisionText;
	public static string Decision = "";
	
	// number of seconds to complete move
	public float Speed = 1f;

	// difference between player and enemy fight results - needed for fate token system
	protected static int FightDiff;

}
