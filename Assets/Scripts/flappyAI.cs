using UnityEngine;
using System.Collections;

public class flappyAI : MonoBehaviour {
	birdController BC;

	// Use this for initialization
	void Start () {
		BC = GetComponent<birdController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float chance = Random.Range(0,100);
		if(chance < 10){
			BC.Flap();
		}
	}
}
