using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerController : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public Rigidbody fishBullet;
    public int lives;

    gunTracker guns;

    bool leftIsShootingAuto = false;
    bool rightIsShootingAuto = false;

    public enum GunType
    {
        hand = 0,
        semiAuto = 1,
        auto = 2

    };

    public GunType leftGunType;
    public GunType rightGunType;

    // Use this for initialization
    void Start()
    {
        lives = 5;
        guns = new gunTracker();
    }

    // Update is called once per frame
    void Update()
    {      
        if (lives <= 0)
        {
            Debug.Log("Game over man. Game over.");
            Application.Quit();
        }

        updateGunState(Time.deltaTime);
    }


    public void updateGunState(float deltaTime)
    {
        guns.update(deltaTime);
     //  Debug.Log("Left Fire delay: +"+guns.fireTimerLeft+ " Right Fire delay: +" + guns.fireTimerRight);
        if (leftIsShootingAuto&&guns.canShoot(true,true))
        {
            leftController.SendMessage("TriggerHapticPulse", .65f);
            fireBullet(leftController.transform);
        }

        if (rightIsShootingAuto && guns.canShoot(false, true))
        {
            rightController.SendMessage("TriggerHapticPulse", .65f);
            fireBullet(rightController.transform);
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
                leftGunType = GunType.semiAuto;
                Debug.Log("Switched to fish");
                leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                break;

            case 12:
                leftGunType = GunType.auto;
                Debug.Log("Switched to autofire");
                leftController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                break;

            case 20:
                rightGunType = GunType.hand;
                Debug.Log("Switched to hand");
                rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = true;
                break;

            case 21:
                rightGunType = GunType.semiAuto;
                Debug.Log("Switched to fish");
                rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;
                break;

            case 22:
                rightGunType = GunType.auto;
                Debug.Log("Switched to autofire");
                rightController.GetComponent<VRTK.VRTK_InteractGrab>().enabled = false;                
                break;

            default:
                break;
        }




    }


    void shoot(bool left)
    {
        if (left)
        {
            if (leftGunType == GunType.semiAuto)
            {
                //Debug.Log("Shooting Left");
                fireBullet(leftController.transform);
                leftController.SendMessage("TriggerHapticPulse", .65f);
            }else if (leftGunType == GunType.auto)
            {
                leftIsShootingAuto = true;
            }
        }
        else //right fire button pressed
        {
            if (rightGunType == GunType.semiAuto)
            {
                //Debug.Log("Shooting Right");
                fireBullet(rightController.transform);
                rightController.SendMessage("TriggerHapticPulse", .65f);
            }else if (rightGunType == GunType.auto)
            {
                rightIsShootingAuto = true;
            }
        }

   



    }

    void endShoot(bool left)
    {
        Debug.Log("Done shooting");
        if(left)
        {
            leftIsShootingAuto = false;
        }
        else
        {
            rightIsShootingAuto = false;
        }
    }

    void fireBullet(Transform controller)
    {
        Rigidbody clone;
        clone = Instantiate(fishBullet, controller.position, new Quaternion(0, 0, 0, 0));

        float speed = 15.0f;//TODO: store somewhere better

        Vector3 forward = controller.forward;
        //Debug.Log(forward);
        //forward = Quaternion.Euler(0, 90, 0) * forward;
        //Debug.Log(forward);

       // forward = forward - new Vector3(.5f, 1f, -1f);
        clone.velocity = new Vector3(forward.x * speed,forward.y * speed, forward.z * speed);
        
        //clone.velocity = new Vector3(-.5f* speed, 0f* speed, .9f * speed);
    }
}
