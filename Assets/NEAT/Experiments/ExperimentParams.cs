using UnityEngine;
using System.Collections;

/// <summary>
/// Experiment parameters. This is meant as a substitute for the XML crap.
/// Most likely we should maintain the XML loader.
/// </summary>
public class ExperimentParams : ScriptableObject {

	public int populationSize;
	public int specieCount;
	public string complexityRegulationString;
	public int complexityThreshold;
	public string description;
}
