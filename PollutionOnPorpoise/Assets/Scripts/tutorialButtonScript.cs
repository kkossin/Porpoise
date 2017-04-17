using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButtonScript : MonoBehaviour {
	private Transform buttonText;
	private Transform hiddenText;
	public bool showHidden;

	// Use this for initialization
	void Start () {
		buttonText = this.transform.Find ("buttonText");
		buttonText.gameObject.SetActive (true);
		hiddenText = transform.Find ("hiddenText");
		hiddenText.gameObject.SetActive (false);
		showHidden = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (showHidden) 
		{
			changeText();
		}
	}

	void changeText()
	{
		buttonText.gameObject.SetActive (false);
		hiddenText.gameObject.SetActive (true);
	}

}
