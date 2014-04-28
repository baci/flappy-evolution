using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvolutionStats : MonoBehaviour {

	public List<float> topLengths;
	public List<float> lowLengths;
	public List<float> avgLengths;

	public static EvolutionStats instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		topLengths.Add(0);
		lowLengths.Add(0);
		avgLengths.Add(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnalyzeStats(){
		findTopLength();
		findLowLength();
		findAvgLength();

		graphCreater.instance.UpdateGraph();
	}

	void findTopLength(){
		float top = Mathf.Max(birdStatistics.instance.Distances);
		topLengths.Add(top);
	}

	void findLowLength(){
		float low = Mathf.Min(birdStatistics.instance.Distances);
		lowLengths.Add(low);
	}

	void findAvgLength(){
		float sum =0;
		for( var i = 0; i < birdStatistics.instance.Distances.Length; i++) {
			sum += birdStatistics.instance.Distances[i];
		}
		avgLengths.Add(sum / birdStatistics.instance.Distances.Length);
	}
}
