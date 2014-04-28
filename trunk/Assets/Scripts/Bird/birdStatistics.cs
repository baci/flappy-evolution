using UnityEngine;
using System.Collections;

public class birdStatistics : MonoBehaviour {

	public float[] Scores;
	public float[] Distances;
	public int[] NumFlaps;
	public float[] Fitness;

	public static birdStatistics instance;
	void Awake(){
		instance = this;
	}

	public void Init(int numBirds){
		Scores = new float[numBirds];
		Distances = new float[numBirds];
		NumFlaps = new int[numBirds];
		Fitness = new float[numBirds];
	}

	public void BirdDied(float score, float distance, int numFlaps, int ID){
		Scores[ID] = score;
		Distances[ID] = distance;
		NumFlaps[ID] = numFlaps;

		Fitness[ID] = Mathf.Max(0,distance-(numFlaps*0.1f));
	}

	public void Clear(){
		Scores = new float[Scores.Length];
		Distances = new float[Distances.Length];
		NumFlaps = new int[NumFlaps.Length];
		Fitness = new float[Fitness.Length];
	}
}
