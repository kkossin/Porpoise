using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    public GameObject Player;

    PlayerController playerScript;




    float throwMult = 1.5f;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        playerScript = Player.GetComponent<PlayerController>();
    }

    private void SetCollidingObject(Collider col)
    {
        if(col.CompareTag("Grabby"))//return if not grabby
        {
            // 1
            if (collidingObject || !col.GetComponent<Rigidbody>())
            {
                return;
            }
            // 2
            collidingObject = col.gameObject;
        }
      
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            //Vector3 scaledVel = Controller.velocity;
            // Vector3 scaledAngVel = Controller.angularVelocity;
            //Debug.Log("Orig: " + scaledVel);
            
            //Debug.Log("Scaled: " + scaledVel);
            Vector3 scaler = new Vector3(this.throwMult, this.throwMult, this.throwMult);
            Vector3 orig = Controller.velocity;
                Vector3 scaledVel= Vector3.Scale(orig, scaler);
            Debug.Log("Orig: " + orig);
            Debug.Log("scaled: " + scaledVel);

            objectInHand.GetComponent<Rigidbody>().velocity = Vector3.Scale(Controller.velocity, scaler);
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Vector3.Scale(Controller.angularVelocity, scaler);
        }
        // 4
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {


        if (Controller.GetHairTriggerDown())
         {
             //Component playerScript = Player.GetComponent<PlayerController>();

                    if (collidingObject)
                    {
                        GrabObject();
                     }
               }

        if (Controller.GetHairTriggerUp())
              {
                   if (objectInHand)
                   {
                        ReleaseObject();
                    }
                }
           

            //  //As this script is disabled from player controller there is no need to check for gun type.
            //  //1
            //if (playerScript.guntype == PlayerController.GunType.hand)
            //  {
            //      //Debug.Log("Just a hand...");
            //      if (Controller.GetHairTriggerDown())
            //      {
            //          //Component playerScript = Player.GetComponent<PlayerController>();

            //          if (collidingObject)
            //          {
            //              GrabObject();
            //          }
            //      }


            //      //2
            //     if (Controller.GetHairTriggerUp())
            //      {
            //          if (objectInHand)
            //          {
            //              ReleaseObject();
            //          }
            //      }
            //  }

            //  //else if (playerScript.guntype == PlayerController.GunType.starFish)
            //  {
            //      if (Controller.GetHairTriggerDown())
            //      {
            //          Debug.Log("I'm a starfish!");
            //      }
    //}
}

  
}
