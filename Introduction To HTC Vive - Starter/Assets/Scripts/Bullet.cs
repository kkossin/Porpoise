using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
     if(other.CompareTag("Destructable"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
   
   
    // Update is called once per frame
    void Update () {
		
	}
}
