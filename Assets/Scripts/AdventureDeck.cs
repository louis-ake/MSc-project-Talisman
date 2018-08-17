using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureDeck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FightBandit(int PlayerStrength)
	{
		var banditResult = 4 + Random.Range(1, 7);
		var playerResult = PlayerStrength + Random.Range(1, 7);
	}
}
