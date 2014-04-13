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

		ISignalArray inputSignal = box.InputSignalArray;
		ISignalArray outputSignal = box.OutputSignalArray;


		return FitnessInfo.Zero;
	}

	public void Reset(){
		
	}
}
