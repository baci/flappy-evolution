using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {


	// Update is called once per frame
	void FixedUpdate () {
		if(gameController.instance.isSimulating)
		transform.position += Vector3.right*gameController.instance.forwardSpeed;

		//transform.eulerAngles += Vector3.forward;
	}

}
