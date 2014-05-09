using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class birdController : MonoBehaviour {

	public float flapForce;

	public int points;

	public int ID;
	public int speciesID;

	public bool dead;
	public bool wait;

	public bool enabled;

	public birdVision vision;
	public flappyAI AI;

	public Vector2 velocity = Vector2.zero;

	GameObject prevPoint;

	public LayerMask mask;

	Vector3 pos = Vector3.zero;
		
	Collider2D col;

	bool hasFlapped = false;

	public float timeSinceLastFlap;

	public int numLives;

	public class GenerationStats{
		public List<float> Scores = new List<float>();
		public List<float> Distances = new List<float>();
		public List<int> NumFlaps = new List<int>();

		public void Setup(int numRounds){
			Scores.Clear();
			Distances.Clear();
			NumFlaps.Clear();
		}
	}

	public GenerationStats generationStats;

	public BirdStats birdStats = new BirdStats();
	public class BirdStats{
		//the accuricy of the vision; lower is better
		public float vision;

		public float size = 1;
		public float startFood = 1;
	}
	public BirdRuntime birdRuntime = new BirdRuntime();
	public class BirdRuntime{
		public int numFlaps = 0;
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
	
		if(transform.position.x > gameController.instance.maxDistance){
			SetDead();
		}
		timeSinceLastFlap += Time.deltaTime;
		//if(gameController.instance.isSimulating){
			if(enabled){
				pos = transform.position;
			velocity.y -= (gameController.instance.gravity*gameController.instance.movementMulti)*gameController.instance.movementMulti;
				if(!dead && !wait){
					velocity.x = gameController.instance.forwardSpeed*gameController.instance.movementMulti;
				}

				pos += new Vector3(velocity.x,velocity.y,0);
				transform.position = pos;

				col = Physics2D.OverlapArea(new Vector2(transform.position.x+0.5f,transform.position.y+0.5f),new Vector2(transform.position.x-0.5f,transform.position.y-0.5f),mask);
				if(!dead && !wait){
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
		if(!dead && !hasFlapped && !wait){
			timeSinceLastFlap = 0;
			velocity = new Vector2(velocity.x,flapForce*(amount*2)*gameController.instance.movementMulti);
			hasFlapped = true;
			birdRuntime.numFlaps ++;
		}else{
			//velocity.x *= 0.5f;
			//velocity.y -= 0.01f;
		}
	}
	

	void destroyMe(){
		if(dead){
			gameController.instance.birdsAlive--;

		}else if(wait){

			gameController.instance.birdsActive--;
		}

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

	public void birdReset(){

	}

	public void SetDead(){
		if(numLives == 0){
			if(!dead){
				//print("DEAD");
				velocity = new Vector2(velocity.x*Random.Range(-0.5f,-1.5f),velocity.y*Random.Range(-0.5f,-1.5f));
				dead = true;

				//calculate avg to send

				generationStats.Scores.Add (points);
				generationStats.Distances.Add(transform.position.x);
				generationStats.NumFlaps.Add(birdRuntime.numFlaps);

				float aPoints = (float)generationStats.Scores.Average();
				float aDistances = (float)generationStats.Distances.Average();
				int aNumFlaps = (int)generationStats.NumFlaps.Average();

				birdStatistics.instance.BirdDied(aPoints, aDistances,aNumFlaps, ID, speciesID);
				vision.enabled = false;
				gameController.instance.numSpeciseLeft[speciesID]--;
				//gameController.instance.birdsAlive--;
			}
		}else{
			if(!wait){
				//print("WAIT");

				velocity = new Vector2(velocity.x*Random.Range(-0.5f,-1.5f),velocity.y*Random.Range(-0.5f,-1.5f));
				wait = true;
				numLives --;

				gameController.instance.numSpeciseLeft[speciesID]--;

				generationStats.Scores.Add (points);
				generationStats.Distances.Add(transform.position.x);
				generationStats.NumFlaps.Add(birdRuntime.numFlaps);

				vision.enabled = false;


			}

		}


	}
	
	
}
