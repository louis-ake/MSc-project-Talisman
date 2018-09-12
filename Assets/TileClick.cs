using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileClick : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		TileTextT.text = TileText;
	}
	
	// Update is called once per frame
	void Update ()
	{
		TileTextT.text = TileText;
	}

	public Text TileTextT;
	public static string TileText = "";
}
