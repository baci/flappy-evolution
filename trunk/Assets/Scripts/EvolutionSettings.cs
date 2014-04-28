using UnityEngine;
using System.Collections;

public class EvolutionSettings : MonoBehaviour {


	public int PopulationSize;
	public int SpecieCount;
	public string ComplexityRegulationStrategy;
	public int ComplexityThreshold;
	public string description;
	public ActivationScheme activationScheme;

	[System.Serializable]
	public class ActivationScheme
	{
		public string scheme;
		public int iters;
		public double threshold;
		public int maxIters;
	}

	public int numInputs;

	public static EvolutionSettings instance;
	void Awake(){
		instance = this;
	}
}
