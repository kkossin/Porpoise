using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour {

    public Vector3 direction;
    public bool fired = false;
    private float speed = 0.03f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (fired)
        {
            Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);
            float newX = this.transform.position.x + speed * direction.x;
            float newY = this.transform.position.y + speed * direction.y;
            float newZ = this.transform.position.z + speed * direction.z;
            this.transform.position = new Vector3(newX, newY, newZ);
        }

        if (this.transform.position.y < 0)
        {
            Destroy(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //This tag doesn't exist yet
        {
            //TODO = blur player's vision
        }
    }

}
