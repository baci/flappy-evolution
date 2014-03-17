using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	Transform bird;

	// Use this for initialization
	void Start () {
		bird = GameObject.Find("Bird").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(bird != null){
			transform.position = new Vector3(bird.position.x,0,-10);
		}
	}
}
