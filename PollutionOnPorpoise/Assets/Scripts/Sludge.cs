﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour {

    public Vector3 direction;
    public bool fired = false;
    private float speed = 0.03f;
    public GameObject effect;
    public AudioClip impactSound;
   // public AudioSource source;
    // Use this for initialization
    void Awake ()
    {
        Destroy(gameObject, 15.0f);
    }

    void Start () {
		transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (fired)
        {
           // Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);
            float newX = this.transform.position.x + speed * direction.x;
            float newY = this.transform.position.y + speed * direction.y;
            float newZ = this.transform.position.z + speed * direction.z;
            this.transform.position = new Vector3(newX, newY, newZ);
        }

        if (this.transform.position.y < 0)
        {
            Destroy(this);
            return;
        }

        /*
        if(GetComponent<LevelManager>().boatsInLevel < 0)
        {
            Destroy(this);//remove all sludge after winning
        }
        */
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
           other.GetComponent<AudioSource>().PlayOneShot(impactSound);
            //Debug.Log("You done been hit, son!");
            Instantiate(effect);
            GameObject.Find("Player").GetComponent<PlayerController>().lives--;
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }
    }
}
