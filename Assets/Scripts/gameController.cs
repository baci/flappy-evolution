using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public float gravity;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = new Vector2(0,-gravity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
