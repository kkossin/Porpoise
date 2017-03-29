using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject leftController;
    public GameObject rightController;

    public Rigidbody fishBullet;
    public int lives;

    public enum GunType
    {
        hand =0,
        starFish =1,
        
    };

    public GunType leftGunType;
    public GunType rightGunType;

    // Use this for initialization
    void Start () {
        lives = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (lives <= 0)
        {
            Debug.Log("Game over man. Game over.");
            Application.Quit();
        }
	}

    public void changeGunType(int button)
    {
            //a 1 in the tens place = left hand
            //a 2 in the tens place = right hand
            //second digit = weapon type selected

            switch (button)
            {
                case 10:
                    leftGunType = GunType.hand;
                    Debug.Log("Switched to hand");
                    leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = true;
                    break;

                case 11:
                    leftGunType = GunType.starFish;
                    Debug.Log("Switched to fish");
                    leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                    break;

                case 12:
                    break;

                case 20:
                    rightGunType = GunType.hand;
                    Debug.Log("Switched to hand");
                    rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = true;
                    break;

                case 21:
                rightGunType = GunType.starFish;
                    Debug.Log("Switched to fish");
                    rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                    break;

                case 22:
                    break;

                default:
                    break;
            }

     
          

    }
    

    void shoot(bool left)
    {
        if(left && leftGunType!=GunType.hand)
        {
            Debug.Log("Shooting Left");
            fireSemiAuto(leftController.transform);
            leftController.SendMessage("TriggerHapticPulse", .65f);
        }
        else if(!left && rightGunType!=GunType.hand)
        {
            Debug.Log("Shooting Right");
            fireSemiAuto(rightController.transform);
            rightController.SendMessage("TriggerHapticPulse", .65f);
        }

        

    }

    void fireSemiAuto(Transform controller)
    {
        Rigidbody clone;
        clone = Instantiate(fishBullet, controller.position,new Quaternion(0,0,0,0));

        float speed = 15.0f;//TODO: store somewhere better
        clone.velocity = new Vector3(controller.forward.x*speed, controller.forward.y*speed, controller.forward.z*speed);
    }
}
