using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishBullet : MonoBehaviour {
    float lifeTime = 30.0f;
    int damage = 5;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Use this for initialization
    void Start () {
      
    }

    // Update is called once per frame
    void Update () {
		
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
                Destroy(this.gameObject);
                target.hitPoints -= damage;
            }
        }
    }
}
