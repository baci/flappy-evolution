using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

	public float flapForce;

	public int points;

	public int ID;

	public bool dead;

	public bool enabled;

	birdVision vision;

	public Vector2 velocity = Vector2.zero;

	GameObject prevPoint;

	public LayerMask mask;

	// Use this for initialization
	void Start () {
		vision = GetComponent<birdVision>();

		//renderer.material.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled){
			if(gameObject.transform.position.y < -5){
				destroyMe();
			}

			//animation
			transform.right = velocity+new Vector2(10,0);
		}
	}

	void FixedUpdate(){
		if(enabled){
			Vector3 pos = transform.position;
			velocity.y -= gameController.instance.gravity/60;
			pos.x += velocity.x/60;
			pos.y += velocity.y/60;
			transform.position = pos;

			Collider2D col = Physics2D.OverlapArea(new Vector2(transform.position.x+0.5f,transform.position.y+0.5f),new Vector2(transform.position.x-0.5f,transform.position.y-0.5f),mask);
			//print(col);
			if(!dead){
				if(col){
					if(col.tag == "PointTrigger"){
						if(prevPoint != col.gameObject){
							prevPoint = col.gameObject;
							points++;
						}
					}else if(col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground"){
	//					print(col);
						velocity*=-1;
						SetDead();
					}
				}
			}

			if(!dead){
				velocity = new Vector2(gameController.instance.forwardSpeed,velocity.y);
		}
		}
	}

	public void Flap(){
		if(!dead){
			velocity = new Vector2(velocity.x,flapForce);
		}else{
			velocity.x *= 0.5f;
			velocity.y -= 1;
		}
	}



	void destroyMe(){
		enabled = false;
	}

	/*
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground"){
			SetDead();
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag =="PointTrigger"){
			points ++;
		}
	}
	*/

	void SetDead(){
		if(!dead){
			dead = true;
			birdStatistics.instance.BirdDied(points, transform.position.x, ID);
			vision.enabled = false;
			gameController.instance.birdsAlive--;
		}
	}
	
	
}
