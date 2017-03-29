using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {

    float lifeTime = 1.0f;
   

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
     }
}
