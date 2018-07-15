using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterTileClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Clicks = 0;

    void OnMouseDown() {
        Clicks += 1;
        Debug.Log("Clicked on Tile " + this.name + " " + Clicks + " times");
    }
}
