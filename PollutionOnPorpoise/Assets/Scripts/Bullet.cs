using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 scale, scaleTarget;
    float xScaleTarget, yScaleTarget, zScaleTarget;
    int damage = 50;
    bool isColliding = false;
    bool expand = false;
    float speed = 1.0f;

	void Start () {
        scale = transform.localScale;
        scaleTarget = scale * 10;
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
        else if (other.CompareTag("Ocean"))
        {
            if (transform.position.x > 3 || transform.position.x < -3 || transform.position.z > 3 || transform.position.z < -3)
            {
                expand = true;
            }
        }
    }   
   
    void Update () {
        isColliding = false;
        
        if (transform.localScale == scaleTarget)
        {
            expand = false;
        }

        if (expand) {
            //might need to replace first parameter with "scale"
            transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, speed * Time.deltaTime);
        }
    }
}
