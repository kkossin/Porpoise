using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {
	public bool started;

    // Use this for initialization
    void Start () {
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void startGame()
	{
		started = true;
	}
}
