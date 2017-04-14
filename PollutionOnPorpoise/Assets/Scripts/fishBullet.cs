using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishBullet : MonoBehaviour {
    float lifeTime = 30.0f;
    public int damage = 5;
    public float speed = 20f;
	public AudioClip impactSound;

    public GameObject explosion;   
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
        
		if (other.CompareTag ("Destructable")) {
			Ship target = other.gameObject.GetComponent<Ship> ();
			other.gameObject.GetComponent<AudioSource> ().PlayOneShot (impactSound);
			if (target.hitPoints <= damage)
            {
                Vector3 pos = other.transform.position;
                Destroy(other.gameObject);
                Instantiate(explosion, pos + new Vector3(0, .5f, 0), new Quaternion(0, 0, 0, 0));
                Destroy (this.gameObject);


			} 
			else 
			{
				Destroy (this.gameObject);
				target.hitPoints -= damage;
			}
		}
		else if (other.CompareTag ("Exit")) 
		{
			Application.Quit ();
		} 
		else if (other.CompareTag ("Tutorial"))
		{
			tutorialButtonScript tbs = other.gameObject.GetComponent<tutorialButtonScript> ();
			tbs.showControls = true;
		}
		else if (other.CompareTag ("start"))
		{
			StartScript ss = other.gameObject.GetComponent<StartScript> ();
			ss.started = true;
		}
    }
}
