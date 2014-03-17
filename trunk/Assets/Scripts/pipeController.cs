using UnityEngine;
using System.Collections;

public class pipeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x+10 < Camera.main.transform.position.x){
			pipeGenerator.instance.generate();
			Destroy(gameObject);
		}
	}
}
