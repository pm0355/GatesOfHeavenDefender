using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public float overallScore = 0;

	void Start () {
	}

	void Update () {
		overallScore+= Time.deltaTime;
	}
	void OnGUI(){
		GUI.Box(new Rect(620,15,130,25),"Score: "+overallScore.ToString("f0"));
	}
}