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
		int i = gc.allBrains.IndexOf(box);
		if(i<0) return FitnessInfo.Zero;

		Debug.Log("Evaluating bird " + i);
		float dist = birdStatistics.instance.Distances[i];

		FitnessInfo fitnessInfo = new FitnessInfo(dist*dist, dist*dist);

		return fitnessInfo;
	}

	public void Reset(){
		
	}
}
