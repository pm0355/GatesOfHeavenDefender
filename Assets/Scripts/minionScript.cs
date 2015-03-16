using UnityEngine;
using System.Collections;

public class minionScript : MonoBehaviour {

	public float currentHealth = 2;
	public float speed = 1.1f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//minions move forward at a constant rate
		transform.position += transform.forward * speed * Time.deltaTime;
	}
	void OnTriggerEnter(Collider other) {
		//If the thing we just collided with is 'damagable' (a tag I created and applied to the tower, other minions, and the Tower), assume 
		//we ran into the wall and make its health smaller.
		if (other.gameObject.tag == "tower") {
			towerScript tw = other.gameObject.GetComponent<towerScript>();
			tw.currentHealth -= 1;
			
			//If the wall's health is 0, destroy it.
			if (tw.currentHealth <= 0) {
				Destroy(other.gameObject);
			}

			//Self-destruct
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Finish") {
			//Self-destruct
			Destroy(gameObject);
		}
	}
}