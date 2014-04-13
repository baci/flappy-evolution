using UnityEngine;
using System.Collections;

/// <summary>
/// This is the experiment. Run this badass mofo and see what happens.
/// </summary>
public class FlappyExperiment : SimpleNeatExperiment
{

	public override int InputCount {
		get {
			return 5;
		}
	}

	public override int OutputCount {
		get {
			return 1;
		}
	}

	public override SharpNeat.Core.IPhenomeEvaluator<SharpNeat.Phenomes.IBlackBox> PhenomeEvaluator {
		get {
			return new BirdsEvaluation();
		}
	}

	public override bool EvaluateParents {
		get {
			return true;
		}
	}
}
