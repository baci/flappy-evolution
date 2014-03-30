using SharpNeat.Core;
using SharpNeat.Phenomes;
using System.Diagnostics;

public class XOReval : IPhenomeEvaluator<IBlackBox> {

	const double STOP_FITNESS = 10.0; //used to check if experiment was a success
	ulong _evalCount;
	bool _stopConditionSatisfied;

	public ulong EvaluationCount { get{return _evalCount;} }
	public bool StopConditionSatisfied { get{return _stopConditionSatisfied;} }

	/// <summary>
	/// Evaluate the specified phenome.
	/// Based on the XOR experiment from SharpNeat
	/// </summary>
	/// <param name="phenome">Phenome.</param>
	public FitnessInfo Evaluate(IBlackBox box){
		double fitness = 0.0;
		double output;
		double pass = 10.0; //making it 10 instead of multiplying later since it is pretty much used as a bool.

		ISignalArray inputSignal = box.InputSignalArray;
		ISignalArray outputSignal = box.OutputSignalArray;

		_evalCount ++;

		////starts to test
		/// Test 1: 0x0 > 0
		//1. reset
		box.ResetState();
		//2. input
		inputSignal[0] = 0.0;
		inputSignal[1] = 0.0;
		//3. activate
		box.Activate();
		if(!box.IsStateValid){
			return FitnessInfo.Zero;
		}
		//4. output
		output = outputSignal[0];
		fitness += 1.0-output; //Base experiment uses quadratic as default for subtraction. Using linear because yes.
		if(fitness>0.5){
			pass = 0.0;
		}
		
		/// Test 2: 1x0 > 1
		//1. reset
		box.ResetState();
		//2. input
		inputSignal[0] = 1.0;
		inputSignal[1] = 0.0;
		//3. activate
		box.Activate();
		if(!box.IsStateValid){
			return FitnessInfo.Zero;
		}
		//4. output
		output = outputSignal[0];
		fitness += 1.0-output; //Base experiment uses quadratic as default for subtraction. Using linear because yes.
		if(fitness<=0.5){
			pass = 0.0;
		}

		/// Test 3: 1x1 > 0
		//1. reset
		box.ResetState();
		//2. input
		inputSignal[0] = 1.0;
		inputSignal[1] = 1.0;
		//3. activate
		box.Activate();
		if(!box.IsStateValid){
			return FitnessInfo.Zero;
		}
		//4. output
		output = outputSignal[0];
		fitness += 1.0-output; //Base experiment uses quadratic as default for subtraction. Using linear because yes.
		if(fitness>0.5){
			pass = 0.0;
		}
		
		/// Test 4: 0x1 > 1
		//1. reset
		box.ResetState();
		//2. input
		inputSignal[0] = 0.0;
		inputSignal[1] = 1.0;
		//3. activate
		box.Activate();
		if(!box.IsStateValid){
			return FitnessInfo.Zero;
		}
		//4. output
		output = outputSignal[0];
		fitness += 1.0-output; //Base experiment uses quadratic as default for subtraction. Using linear because yes.
		if(fitness<=0.5){
			pass = 0.0;
		}

		//Tests are made, now adding the pass score if everything went well
		fitness += pass;
		if(fitness >= STOP_FITNESS){
			_stopConditionSatisfied = true;
		}

		//done!
		return new FitnessInfo(fitness, fitness);
	}

	/// <summary>
	/// Reset this instance.
	/// </summary>
	public void Reset(){

	}
}
