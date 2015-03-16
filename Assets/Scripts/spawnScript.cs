using UnityEngine;
using System.Collections;

public class spawnScript : MonoBehaviour {

	public GameObject thingToMake;
	int numberOfMinions = 5;
	public float spawnTimer = 9;
	float timer = 1f;
	int startingNumberOfMinions = 0;	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		startingNumberOfMinions = numberOfMinions;
		if (spawnTimer <= 0) {
			timer -= Time.deltaTime;
			if (timer <= 0){
				startingNumberOfMinions--;
				Spawn();
				timer = 1f;
				if (startingNumberOfMinions <= 0){
					numberOfMinions++;
					spawnTimer = 9;
					startingNumberOfMinions = numberOfMinions;
					timer = 1f;
				}
			}
		}
		else {
			spawnTimer -= Time.deltaTime;
		}
	}

	void Spawn () {
		GameObject go = (GameObject)Instantiate (thingToMake, transform.position, Quaternion.identity);
		go.transform.forward = transform.forward;
	}
}
