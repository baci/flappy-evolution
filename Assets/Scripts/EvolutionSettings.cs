using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
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
	public int numOutputs;

	public static EvolutionSettings instance;
	void Awake(){
		instance = this;
	}

	void OnValidate(){
		ComplexityThreshold = Mathf.Max(ComplexityThreshold,(numInputs+numOutputs)*3);
		SpecieCount = Mathf.Min(SpecieCount,PopulationSize);
		SpecieCount = Mathf.Max(SpecieCount,1);
		PopulationSize = Mathf.Max(PopulationSize,1);
		//print((numInputs+numOutputs)*3);
	}
}
