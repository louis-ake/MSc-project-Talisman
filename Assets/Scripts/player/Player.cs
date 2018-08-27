﻿using System;
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
	public static bool won = true;
	public static bool done = false;
	public static bool moved;
	public static bool actionNeeded; 

	// For displaying player decisions
	public Text DecisionText;
	public static string Decision = "";
	
	// number of seconds to complete move
	public float Speed = 1f;

	public static int FightDiff;

}
