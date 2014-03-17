using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.position += Vector3.right*gameController.instance.forwardSpeed*Time.deltaTime;
	}

}
