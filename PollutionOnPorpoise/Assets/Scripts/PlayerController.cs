using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public GameObject leftControllerModel;
    public GameObject rightControllerModel;

    public Rigidbody fishBullet;
    public Rigidbody fishBulletAuto;

    public int lives;

    gunTracker guns;

    bool leftIsShootingAuto = false;
    bool rightIsShootingAuto = false;

    bool leftShieldOn = false;
    bool rightShieldOn = false;
    public GameObject leftShield;
    public GameObject rightShield;
    private GameObject activeLeftShield;
    private GameObject activeRightShield;

    public enum GunType
    {
        hand = 0,
        semiAuto = 1,
        auto = 2,
        shield = 3
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
            //Debug.Log("Game over man. Game over.");
            gameOver();
        }
        updateGunState(Time.deltaTime);
        updateShieldState();
    }

    public void updateGunState(float deltaTime)
    {
        guns.update(deltaTime);
        //Debug.Log("Left Fire delay: +"+guns.fireTimerLeft+ " Right Fire delay: +" + guns.fireTimerRight);
        if (leftIsShootingAuto&&guns.canShoot(true,true))
        {
            leftController.SendMessage("TriggerHapticPulse", .65f);
            fireBullet(leftController.transform,leftGunType);
        }

        if (rightIsShootingAuto && guns.canShoot(false, true))
        {
            rightController.SendMessage("TriggerHapticPulse", .65f);
            fireBullet(rightController.transform,rightGunType);
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

            case 13:
                leftGunType = GunType.shield;
                Debug.Log("Switched to shield");
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

            case 23:
                rightGunType = GunType.shield;
                Debug.Log("Switched to shield");
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
                fireBullet(leftController.transform,leftGunType);
                leftController.SendMessage("TriggerHapticPulse", .65f);
            }
            else if (leftGunType == GunType.auto)
            {
                leftIsShootingAuto = true;
            }
        }
        else //right fire button pressed
        {
            if (rightGunType == GunType.semiAuto)
            {
                //Debug.Log("Shooting Right");
                fireBullet(rightController.transform,rightGunType);
                rightController.SendMessage("TriggerHapticPulse", .65f);
            }
            else if (rightGunType == GunType.auto)
            {
                rightIsShootingAuto = true;
            }
        }
    }

    void endShoot(bool left)
    {
        Debug.Log("Done shooting");
        if (left)
        {
            leftIsShootingAuto = false;
        }
        else
        {
            rightIsShootingAuto = false;
        }
    }

    void fireBullet(Transform controller, GunType fireMode)
    {
        Rigidbody clone;
        
        if(fireMode==GunType.semiAuto)
        {
            clone = Instantiate(fishBullet, controller.position, new Quaternion(0, 0, 0, 0));
        }
        else if(fireMode==GunType.auto)
        {
            clone = Instantiate(fishBulletAuto, controller.position, new Quaternion(0, 0, 0, 0));
        }
        else//default
        {
            clone = Instantiate(fishBullet, controller.position, new Quaternion(0, 0, 0, 0));
        }



        float speed = fishBullet.GetComponent<fishBullet>().speed;

        Vector3 forward = controller.forward;
        //Debug.Log(forward);
        //forward = Quaternion.Euler(0, 90, 0) * forward;
        //Debug.Log(forward);

       // forward = forward - new Vector3(.5f, 1f, -1f);
        clone.velocity = new Vector3(forward.x * speed,forward.y * speed, forward.z * speed);
        
        //clone.velocity = new Vector3(-.5f* speed, 0f* speed, .9f * speed);
    }

    void updateShieldState()
    {
        if (leftGunType == GunType.shield && !leftShieldOn)
        {
            Vector3 positionLeft = leftControllerModel.transform.position;
            Quaternion rotationLeft = leftControllerModel.transform.rotation;
            activeLeftShield = Instantiate(leftShield, positionLeft, rotationLeft, leftControllerModel.transform);
            activeLeftShield.transform.Rotate(0, -90, 0);
            activeLeftShield.transform.Rotate(180, 0, 0);
            leftShieldOn = true;
        }
        else if (rightGunType == GunType.shield && !rightShieldOn)
        {
            Vector3 positionRight = rightControllerModel.transform.position;
            Quaternion rotationRight = rightControllerModel.transform.rotation;
            activeRightShield = Instantiate(rightShield, positionRight, rotationRight, rightControllerModel.transform);
            activeRightShield.transform.Rotate(0, -90, 0);
            activeRightShield.transform.Rotate(180, 0, 0);           
            rightShieldOn = true;
        }
        else if (leftGunType != GunType.shield && leftShieldOn)
        {
            Destroy(activeLeftShield);
            leftShieldOn = false;
        }
        else if (rightGunType != GunType.shield && rightShieldOn)
        {
            Destroy(activeRightShield);
            rightShieldOn = false;
        }
    }

    void gameOver()
    {
        SceneManager.LoadScene("Game");
    }
}
