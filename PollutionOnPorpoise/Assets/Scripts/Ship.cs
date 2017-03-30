using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    private float cx = 0.0f; // coords of center of circle (center of screen, in this case)
    private float cy = 0.0f;
    public bool moving = false;
    public int hitPoints = 100;
    public GameObject sludge;

    private int radius = 16; // radius of circle
    private double angle;

    GameObject headset;
    GameObject simulator;
    private GameObject player;
    private float bulletSpeed;
 
    //int speed = 6; // speed of travel (seconds to make a complete circuit)
    double speedScale = (0.001 * 2 * Math.PI) / .05;

    void Start () {
        //speedScale = (0.001 * 2 * Math.PI) / speed;
        headset = GameObject.Find("[CameraRig]");
        simulator = GameObject.Find("VRSimulatorCameraRig");

        angle = UnityEngine.Random.Range(0, 2.0f * (float)Math.PI);
        float offset = UnityEngine.Random.Range(0, 5.0f);
        InvokeRepeating("shoot", 5.0f + offset, 5.0f); //starting in 5+ seconds, shoot will be called every 5 seconds

		if (headset != null && headset.activeSelf)
		{
			player = headset.transform.GetChild(2).gameObject;
		}
		else
		{
			player = simulator;
		}
    }
   
    void Update () {
        if (moving)
        {
            angle += Time.deltaTime * speedScale;
            //Debug.Log("angle: " + angle);
            //Debug.Log("speedScale: " + speedScale);
            //Debug.Log("angle cos: " + Math.Cos(angle));
            //Debug.Log("angle sin: " + Math.Sin(angle));
            transform.position = new Vector3((float)(cx + Math.Sin(angle) * radius), 1.5f, (float)(cy + Math.Cos(angle) * radius));
            //this._x = cx + Math.Sin(angle) * radius;
            //this._y = cy + Math.Cos(angle) * radius;

			//Rotation
			Vector3 rotateDirection = (player.transform.position - transform.position).normalized;
			transform.rotation = Quaternion.LookRotation(rotateDirection);
			transform.Rotate(0, 90, 0);
        }
    }

    public void shoot ()
    {
        if (this.transform.position.y > 0.0f)
        {     
            Vector3 shotDirection = (player.transform.position - transform.position).normalized;
            Sludge newSludge = Instantiate(sludge).GetComponent<Sludge>();
            newSludge.transform.position = this.transform.position;
            newSludge.direction = shotDirection;
            newSludge.fired = true;           
        }
    }

    public void toggleMoving()
    {
        moving = !moving;
    }
}
