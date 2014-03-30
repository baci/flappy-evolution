using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

	public float flapForce;

	public int points {get;private set;}

	public int ID;

	bool dead;

	birdVision vision;

	// Use this for initialization
	void Start () {
		vision = GetComponent<birdVision>();

		renderer.material.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
	}
	
	// Update is called once per frame
	void Update () {

		if(gameObject.transform.position.y < -5){
			destroyMe();
		}

		//animation
		transform.right = rigidbody2D.velocity+new Vector2(10,0);
	}

	void FixedUpdate(){
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
		if(col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground"){
			dead = true;
			collider2D.isTrigger = true;
			birdStatistics.instance.BirdDied(points, transform.position.x, ID);
			vision.enabled = false;
			gameController.instance.birdsAlive--;
			GetComponent<BoxCollider2D>().enabled = false;
			rigidbody2D.drag = 2;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag =="PointTrigger"){
			points ++;
		}
	}
	
}
