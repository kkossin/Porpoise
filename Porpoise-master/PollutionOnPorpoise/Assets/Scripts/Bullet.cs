using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    int damage = 50;
    bool isColliding;
	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (isColliding) { return; }
        isColliding = true;
               if (other.CompareTag("Destructable"))
        {
            Ship target = other.gameObject.GetComponent<Ship>();
            Debug.Log(target.hitPoints);
            if (target.hitPoints <= damage)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                target.hitPoints -= damage;
            }
        }
    }   
   
    // Update is called once per frame
    void Update () {
        isColliding = false;
	}
}
