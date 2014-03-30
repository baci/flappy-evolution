using UnityEngine;
using System.Collections;

/// <summary>
/// This is the experiment. Run this badass mofo and see what happens.
/// </summary>
public class XORexperiment : SimpleNeatExperiment {

	public override int InputCount {
		get {
			return 2;
		}
	}

	public override int OutputCount {
		get {
			return 1;
		}
	}

	public override SharpNeat.Core.IPhenomeEvaluator<SharpNeat.Phenomes.IBlackBox> PhenomeEvaluator {
		get {
			return new XOReval();
		}
	}

	public override bool EvaluateParents {
		get {
			return true;
		}
	}
}
