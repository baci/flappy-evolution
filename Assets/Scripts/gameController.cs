using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public float gravity;
	public float forwardSpeed;

	public int numBirds;

	public GameObject birdPre;

	public int birdsAlive;

	GameObject birdHolder;

	public static gameController instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		Physics2D.gravity = new Vector2(0,-gravity);

		birdHolder = new GameObject();
		birdHolder.name = "birdHolder";

		birdStatistics.instance.Init(numBirds);

		newRound();
	}

	void newRound(){
		Vector3 campos = Camera.main.transform.position;
		campos.x = 0;
		Camera.main.transform.position = campos;

		pipeGenerator.instance.Clear();
		pipeGenerator.instance.GenerateStart();

		birdStatistics.instance.Clear();
		birdsAlive = numBirds;

		for(int i=0;i< numBirds;i++){
			GameObject g = Instantiate(birdPre) as GameObject;
			g.transform.parent = birdHolder.transform;
			birdController bc = g.GetComponent<birdController>();
			bc.ID = i;
			setBirdStats(bc);
		}
	}

	void setBirdStats(birdController bc){

	}

	// Update is called once per frame
	void Update () {
		if(birdsAlive == 0){
			newRound();
		}
	}
}
