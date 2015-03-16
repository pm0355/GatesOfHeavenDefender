using UnityEngine;
using System.Collections;

public class mousemove : MonoBehaviour {
	float shootTimer = 0.35f;
	public GameObject thingToShoot;
	float speed = 10f;
	float starttime = 3f;
	public bool hold = true;

	public Transform target;
	public Vector3 targetPostion;

	// Use this for initialization
	CharacterController cc;

	void Start () {
		cc = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 amountToMove  = Vector3.zero;
		if(Input.GetMouseButton(0)) {
			transform.Translate (0, 0, (Time.deltaTime * speed));

			if (Input.GetMouseButtonUp (0)) {
			hold = false;
				}

				}


			hold = true;
		shootTimer -= Time.deltaTime;
		if(Input.GetMouseButtonDown(1) && shootTimer <= 0){
				shootTimer = 0.0f;
				GameObject go = (GameObject)Instantiate(thingToShoot, transform.position, Quaternion.identity);
				go.transform.forward = transform.forward;
				//Raise the position so it is off the ground
				go.transform.position += Vector3.up * 1f;
				//Destroy the object in three seconds
				Destroy(go, 3f);
			}
	
	}
	}




