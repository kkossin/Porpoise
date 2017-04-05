using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButtonScript : MonoBehaviour {
	private Transform howToText;
	private Transform controlsText;
	public bool showControls;

	// Use this for initialization
	void Start () {
		howToText = this.transform.Find ("tutorialText");
		howToText.gameObject.SetActive (true);
		controlsText = transform.Find ("tutorialText1");
		controlsText.gameObject.SetActive (false);
		showControls = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (showControls) 
		{
			changeText ();
		}
	}
	void changeText()
	{
		howToText.gameObject.SetActive (false);
		controlsText.gameObject.SetActive (true);
	}

}
