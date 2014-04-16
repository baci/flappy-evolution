using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

	public float flapForce;

	public int points;

	public int ID;

	public bool dead;

	public bool enabled;

	public birdVision vision;
	public flappyAI AI;

	public Vector2 velocity = Vector2.zero;

	GameObject prevPoint;

	public LayerMask mask;

	Vector3 pos = Vector3.zero;
		
	Collider2D col;

	bool hasFlapped = false;


	public BirdStats birdStats = new BirdStats();
	public class BirdStats{
		//the accuricy of the vision; lower is better
		public float vision;

		public float size = 1;
		public float startFood = 1;
	}

	// Use this for initialization
	void Start () {
		vision = GetComponent<birdVision>();
		AI = GetComponent<flappyAI>();

		//renderer.material.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled){
			if(gameObject.transform.position.y < -5){
				destroyMe();
			}

			//animation
			//transform.right = velocity+new Vector2(10,0);
		}
	}

	void FixedUpdate(){
		if(transform.position.x > 50){
			SetDead();
		}
		//if(gameController.instance.isSimulating){
			if(enabled){
				pos = transform.position;
				velocity.y -= gameController.instance.gravity;
				if(!dead){
					velocity.x = gameController.instance.forwardSpeed;
				}

				pos += new Vector3(velocity.x,velocity.y,0);
				transform.position = pos;

				col = Physics2D.OverlapArea(new Vector2(transform.position.x+0.5f,transform.position.y+0.5f),new Vector2(transform.position.x-0.5f,transform.position.y-0.5f),mask);
				if(!dead){
					if(col){
						if(col.tag == "PointTrigger"){
							if(prevPoint != col.gameObject){
								prevPoint = col.gameObject;
								points++;
							}
						}else if(col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground"){
							SetDead();
						}
					}
				}


				//transform.right = new Vector3(velocity.x+0.5f,velocity.y,0);//+Vector2.right*1;
			}
		//}
	}

	void LateUpdate(){
		hasFlapped = false;
	}

	public void Flap(float amount){
		if(!dead && !hasFlapped){
			velocity = new Vector2(velocity.x,flapForce*(amount*2));
			hasFlapped = true;
		}else{
			//velocity.x *= 0.5f;
			//velocity.y -= 0.01f;
		}
	}
	

	void destroyMe(){
		gameController.instance.birdsAlive--;
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

	public void SetDead(){
		if(!dead){
			velocity = new Vector2(velocity.x*Random.Range(-0.5f,-1.5f),velocity.y*Random.Range(-0.5f,-1.5f));
			dead = true;
			birdStatistics.instance.BirdDied(points, transform.position.x, ID);
			vision.enabled = false;
			//gameController.instance.birdsAlive--;
		}
	}
	
	
}
