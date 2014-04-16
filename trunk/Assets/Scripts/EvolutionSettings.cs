using UnityEngine;
using System.Collections;

public class EvolutionSettings : MonoBehaviour {


	public int PopulationSize;
	public int SpecieCount;
	public string ComplexityRegulationStrategy;
	public int ComplexityThreshold;


	public static EvolutionSettings instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
