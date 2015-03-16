using UnityEngine;
using System.Collections;

public class towerScript : MonoBehaviour {

	public int currentHealth = 20;

	// Use this for initialization
	void Start () {
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "takeDamage") {
			currentHealth -= 1;
			}
		}

	// Update is called once per frame
	void Update() {
		if(currentHealth <= 0){
			currentHealth = 0;
			Destroy(gameObject);
		}
	}
	void OnGUI(){
			//if(gameobject.name == "tower1"){}
			GUI.Box (new Rect (150, 15, 130, 25), "Tower 1 health: " + currentHealth.ToString ("f0"));
			//if(gameObject.name == "tower2"{
			GUI.Box (new Rect (320, 15, 130, 25), "Tower 2 health: " + currentHealth.ToString ("f0"));
			//if(gameObject.name == "tower3"{}
			GUI.Box(new Rect(500,15,130,25),"Tower 3 health: "+currentHealth.ToString("f0"));
	}
}