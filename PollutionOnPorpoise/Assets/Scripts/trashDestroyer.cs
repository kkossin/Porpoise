using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TrashEnterEvent : UnityEvent<Collider> { }

public class trashDestroyer : MonoBehaviour {

    public TrashEnterEvent onTrashEnter;
    // Use this for initialization
    void Start () {
     
}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("Trash Found");
            onTrashEnter.Invoke(other);

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
