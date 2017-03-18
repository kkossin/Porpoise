using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    float cx = 0.0f; // coords of center of circle (center of screen, in this case)
    float cy = 0.0f;
    public bool moving = false;
    public int hitPoints = 100;
 
    int radius = 16; // radius of circle
    double angle;
 
    //int speed = 6; // speed of travel (seconds to make a complete circuit)
    double speedScale = (0.001 * 2 * Math.PI) / .05;

    void Start () {
        //speedScale = (0.001 * 2 * Math.PI) / speed;
        angle = UnityEngine.Random.Range(0, 2.0f * (float)Math.PI);
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
        }
    }
    public void toggleMoving()
    {
        moving = !moving;
    }
}
