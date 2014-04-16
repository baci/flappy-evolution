using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

	TextMesh mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = "Length "+(int)transform.position.x+"\n"+
			" Generation "+gameController.instance.generation+"\n"+
				" Birds alive "+gameController.instance.birdsAlive+"\n";
	}
}
