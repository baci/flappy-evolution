using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if(gameController.instance.isSimulating)
		transform.position += Vector3.right*gameController.instance.forwardSpeed*gameController.instance.movementMulti;

		//transform.eulerAngles += Vector3.forward;
	}

}
