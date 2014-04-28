using UnityEngine;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using System.Collections;

public class BirdsEvaluation : IPhenomeEvaluator<IBlackBox> {

	private ulong _evalCount;
	private bool _stopConditionSatisfied;
	
	public ulong EvaluationCount { get{return _evalCount;} }
	public bool StopConditionSatisfied { get{return _stopConditionSatisfied;} }

	public FitnessInfo Evaluate(IBlackBox box){
		//ISignalArray inputSignal = box.InputSignalArray;
		//ISignalArray outputSignal = box.OutputSignalArray;

		gameController gc = gameController.instance;
		int i;// = gc.allBrains.IndexOf(box);
		for(i = 0; i < gc.allBrains.Count; i++){
			// TODO
			//------------------PROBLEM: Box always has id=-1, meaning its a new brain, not from the list. allbrains.id is correct...
			//Debug.Log("BOX "+box.BrainID+" brain "+gc.allBrains[i].BrainID);
			// TODO: why not just try with brainID = i, isnt it the same order?
			if(box.BrainID == gc.allBrains[i].BrainID){
				break;
			}
		}
		
		if(i==gc.allBrains.Count) return FitnessInfo.Zero;

		Debug.Log("Evaluating bird " + i);
		//float dist = birdStatistics.instance.Distances[i];

		float fitness = birdStatistics.instance.Fitness[i];

		FitnessInfo fitnessInfo = new FitnessInfo(fitness,fitness);

		return fitnessInfo;
	}

	public void Reset(){
		
	}
}
