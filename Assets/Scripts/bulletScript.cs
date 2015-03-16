using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	
	public float speed = 40f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Bullets move forward at a constant rate
		transform.position += transform.forward * speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other) {
		//If the thing we just collided with is 'damagable' (a tag I created and applied to the wall), assume 
		//we ran into the wall and make its health smaller.
		if (other.gameObject.name != "Player"){
			if (other.gameObject.tag == "takesDamage") {
				if (other.gameObject.name == "Minion1(Clone)"){
					Hellionscript1 hs1 = other.gameObject.GetComponent<Hellionscript1> ();
					hs1.currentHealth -= 1;
					
					//If the wall's health is 0, destroy it.
					if (hs1.currentHealth <= 0) {
						Destroy (other.gameObject);
					}
				else if (other.gameObject.name == "Minion2(Clone)"){
					Hellionscript2 hs2 = other.gameObject.GetComponent<Hellionscript2> ();
					hs2.currentHealth -= 1;
					
					//If the wall's health is 0, destroy it.
					if (hs2.currentHealth <= 0) {
						Destroy (other.gameObject);
					}
				}
				else if (other.gameObject.name == "Minion3(Clone)"){
					Hellionscript3 hs3 = other.gameObject.GetComponent<Hellionscript3> ();
					hs3.currentHealth -= 1;
					
					//If the wall's health is 0, destroy it.
					if (hs3.currentHealth <= 0) {
						Destroy (other.gameObject);
					}
				}
			}
			else if(other.gameObject.tag == "tower"){
				towerScript tw = other.gameObject.GetComponent<towerScript> ();
				tw.currentHealth -= 1;
				
				//If the tower's health is 0, destroy it.
				if (tw.currentHealth < 0) {
					Destroy (other.gameObject);
				}
			}
			Destroy (gameObject);
			}
		}
	}
}