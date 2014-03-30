using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

	//public float forwardSpeed;

	public float flapForce;

	public int points {get;private set;}

	public int ID;

	bool dead;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Flap();
		}

		if(gameObject.transform.position.y < -5){
			destroyMe();
		}

		transform.right = rigidbody2D.velocity+new Vector2(25,0);

		if(!dead){
			rigidbody2D.velocity = new Vector2(gameController.instance.forwardSpeed,rigidbody2D.velocity.y);
		}
	}

	public void Flap(){
		if(!dead){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,flapForce);
		}
	}



	void destroyMe(){
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col){
		//print ("HELOP");
		if(col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground"){
			dead = true;
			collider2D.isTrigger = true;
			birdStatistics.instance.BirdDied(points, transform.position.x, ID);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag =="PointTrigger"){
			//print("TEST");
			points ++;
		}
	}

	/*
	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2-25,20,150,20), "POINTS "+points);
	}
	*/
}
