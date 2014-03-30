using UnityEngine;
using System.Collections;

public class birdStatistics : MonoBehaviour {

	public float[] Scores;
	public float[] Distances;

	public static birdStatistics instance;
	void Awake(){
		instance = this;
	}

	public void Init(int numBirds){
		Scores = new float[numBirds];
		Distances = new float[numBirds];
	}

	public void BirdDied(float score, float distance, int ID){
		Scores[ID] = score;
		Distances[ID] = distance;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
