using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public float gravity;
	public float forwardSpeed;

	public int numBirds;

	public GameObject birdPre;


	public static gameController instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		Physics2D.gravity = new Vector2(0,-gravity);

		birdStatistics.instance.Init(numBirds);

		for(int i=0;i< numBirds;i++){
			GameObject g = Instantiate(birdPre) as GameObject;
			g.GetComponent<birdController>().ID = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
