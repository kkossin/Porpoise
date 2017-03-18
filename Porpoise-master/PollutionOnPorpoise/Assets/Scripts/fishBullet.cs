using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletManager : MonoBehaviour {
    float lifeTime = 30.0f;
    int damage = 20;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    // Use this for initialization
    void Start () {
      
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructable"))
        {
            Ship target = other.gameObject.GetComponent<Ship>();
            if (target.hitPoints <= damage)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                target.hitPoints -= damage;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
