using UnityEngine;
using System.Collections;


/*
 * This script will give basic navigation and chase movement behavior to a GameObject
 * 
 * It will try to find the player, and if it cannot, it will move towards the furthest point in its fieldOfView
 * 
 * It will also shoot "thingToShoot" at a constant rate
 */ 
public class BotScript : MonoBehaviour {
	//The thing that the bot will shoot
	public GameObject thingToShoot;
	//How often it will be shot
	float shootTimer = 1f;
	
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
		//Update timers
		shootTimer -= Time.deltaTime;
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
				if (hit.collider.gameObject.name == "Player") {
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
		
		
		//Shoot something every 1 second
		if (shootTimer < 0) {
			shootTimer = 1f;
			GameObject go = (GameObject)Instantiate (thingToShoot, transform.position, Quaternion.identity);
			//Raise the position so it is off the ground
			go.transform.position += Vector3.up * 1.5f;
			go.transform.forward = transform.forward;
			//The script attached to bullet controls the spawned object's movement forward and what happens when it 
			//collides with something
		}
		
	}
}










