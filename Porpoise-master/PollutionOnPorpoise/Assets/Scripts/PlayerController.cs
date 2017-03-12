using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject leftController;
    public GameObject rightController;

    public enum GunType
    {
        hand =0,
        starFish =1,
        
    };

    public GunType guntype;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeGunType(int button)
    {
            //a 1 in the tens place = left hand
            //a 2 in the tens place = right hand
            //second digit = weapon type selected

            switch (button)
            {
                case 10:
                    guntype = GunType.hand;
                    Debug.Log("Switched to hand");
                    leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = true;
                    break;

                case 11:
                    guntype = GunType.starFish;
                    Debug.Log("Switched to fish");
                    leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                    break;

                case 12:
                    break;

                case 20:
                    guntype = GunType.hand;
                    Debug.Log("Switched to hand");
                    rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = true;
                    break;

                case 21:
                    guntype = GunType.starFish;
                    Debug.Log("Switched to fish");
                    rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                    break;

                case 22:
                    break;

                default:
                    break;
            }

    }
}
