using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	public GameObject thingToShoot;
	
	public float speed = 50f;
	
	public float jumpSpeed = 0.4f;
	public float gravity = -1f;
	public float yVel = 0f;
	float shootTimer = 0.35f;
	
	bool grounded = false;
	CharacterController cc;
	
	// Use this for initialization
	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//If the player presses left shift, spawn a "bullet" at current position facing forward.
		//The bullets behavior is in the "BulletScript"
		shootTimer -= Time.deltaTime;
		if (Input.GetKey (KeyCode.Space) && shootTimer <= 0) {
			shootTimer = 0.5f;
			GameObject go = (GameObject)Instantiate(thingToShoot, transform.position, Quaternion.identity);
			go.transform.forward = transform.forward;
			//Raise the position so it is off the ground
			go.transform.position += Vector3.up * 1f;
			//Destroy the object in three seconds
			Destroy(go, 3f);
		}
		
		//Rotate is the player presses left or right (or 'a' or 'd')
		float hAxis = Input.GetAxis ("Horizontal");
		transform.Rotate (0,hAxis * 1.5f,0);
		
		//Because we are using a character controlller, instead of changing transform.position directly, we will
		//be making a call to CharacterController's Move function. We will call this at the end of the function.
		//For now, 'amountToMove' will be a Vector3 that tells us how much to move.
		Vector3 amountToMove = Vector3.zero;
		if (Input.GetKey (KeyCode.W)) {
			amountToMove += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			amountToMove -= transform.forward * speed / 2f * Time.deltaTime;
		}
		
		//Jump Code:
		//yVel is a variable that we will use to keep track of how much we should move in the y direction
		//Grounded is set below, but it tells us whether we are colliding with the ground or not
		if (grounded) {
			yVel = 0f;
			//When the jump key is pressed, set yVel to a constant speed.
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				yVel = jumpSpeed;
			}
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			if (yVel > 0) {
				yVel = 0;
			}
		}
		//Every frame, make yVel smaller by adding gravity
		yVel += gravity * Time.deltaTime;
		yVel = Mathf.Clamp(yVel, gravity, jumpSpeed);
		//Set the amountToMove to the yVel that we have been computing
		amountToMove.y = yVel;
		
		//Finally, move the player by amountToMove.
		cc.Move (amountToMove);
		
		//Find out if we are colliding with the ground by doing this bitwise AND operation
		grounded = ((cc.collisionFlags & CollisionFlags.Below) != 0);
	}
}
















