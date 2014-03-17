using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

	public float forwardSpeed;

	public float flapForce;

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

		transform.right = rigidbody2D.velocity;

		if(!dead){
			rigidbody2D.velocity = new Vector2(forwardSpeed,rigidbody2D.velocity.y);
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
		if(col.gameObject.tag == "Pipe"){
			dead = true;
			collider2D.isTrigger = true;
		}
	}
}
