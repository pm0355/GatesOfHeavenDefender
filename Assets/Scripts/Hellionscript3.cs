using UnityEngine;
using System.Collections;

public class Hellionscript3 : MonoBehaviour {
	
	//Health
	public float currentHealth =2;
	
	//How fast the bot will move forward
	float speed = 5f;
	
	//Variables that control how often the bot will choose where to look/move
	float navRate = 1.5f;
	float navTimer;
	
	//How much bot is able to look while considering where to move
	float fieldOfView = 270f;
	//How many rays the bot will use across its fieldOfView when deciding where to move
	float viewResultion = 7f;
	
	//Initialized in Start()
	int layerMask;
	GameObject player;
	CharacterController cc;
	
	// Use this for initialization
	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
		player = GameObject.Find ("Player");
		navTimer = navRate;
		
		//This will be used in all future Raycasts. We only want to have our Rays collide with 
		//things that aren't on the Ignore Raycast Layer
		layerMask = 1 << LayerMask.NameToLayer ("Ignore Raycast");
		layerMask = ~layerMask;
	}
	
	// Update is called once per frame
	void Update () {

		//Death Stuff
		if (currentHealth <= 0) {
			Destroy(gameObject);
		}
		
		//Update timers
		navTimer -= Time.deltaTime;
		
		if (navTimer < 0) {
			//reset navigation timer
			navTimer = navRate;
			
			//Check to see if we "can see" the player.
			//We do this by creating a Ray that looks at the player, then Raycasting in that direction and seeing if the
			//thing the ray collides with first is the player
			Vector3 vectorToPlayer = player.transform.position - transform.position + Vector3.up * 1.5f;
			Ray rayToPlayer = new Ray (transform.position, vectorToPlayer.normalized);
			RaycastHit hit;
			Physics.Raycast (rayToPlayer, out hit, Mathf.Infinity, layerMask);
			bool foundPlayer = false;
			//Debug.DrawRay (transform.position, vectorToPlayer.normalized * 1000f);
			//The code below checks to see what the rayToPlayer collided with
			if (hit.collider != null) {
				Debug.Log (hit.collider.gameObject.name);
				//If the Ray collided with the player, make the bot face the player
				if (hit.collider.gameObject.name == "Player" || hit.collider.gameObject.name =="Tower") {
					transform.forward = vectorToPlayer.normalized;
					foundPlayer = true;
				}
			}
			
			
			if (!foundPlayer) {
				//This means we dind't find the player and want to move to the furtherst point based on raycasting
				RaycastHit furthestHit = new RaycastHit();
				//Make a ray for each (fieldOfView / viewResolution) degress and keep track of the RaycastHit info
				//that is the furthest away. 
				for (int i = 0; i < viewResultion; i++) {
					
					float amountToRotate = Mathf.Lerp(-fieldOfView / 2, fieldOfView / 2, i / viewResultion);
					
					//Remember, when we rotate a vector, we need to put the Quaternion.Euler on the left side of the multiplication
					Vector3 rotatedForward = Quaternion.Euler(0, amountToRotate, 0) * transform.forward;
					Ray ray = new Ray(transform.position, rotatedForward);
					RaycastHit h;
					if (Physics.Raycast(ray, out h, Mathf.Infinity, layerMask)) {
						if (furthestHit.collider == null) {
							furthestHit = h;
						}
						else {
							if (h.distance > furthestHit.distance) {
								furthestHit = h;
							}
						}
					}
				}
				//Once we have looked at all the rays in our fieldOfView, and found the one that collided with the further
				//away thing, look toward where that furthest ray hit.
				transform.forward = (furthestHit.point - transform.position).normalized;
			}
		}
		//Move forward
		cc.Move (transform.forward * speed * Time.deltaTime);
	}
	//Damage Dealing
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

