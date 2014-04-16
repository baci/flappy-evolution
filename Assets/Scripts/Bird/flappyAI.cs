using UnityEngine;
using System.Collections;


public class flappyAI : MonoBehaviour {
	birdController BC;

	public SharpNeat.Phenomes.IBlackBox myBrain;

	// Use this for initialization
	void Start () {
		BC = GetComponent<birdController>();
	}



	// Update is called once per frame
	void FixedUpdate () {
		if(myBrain != null){
			myBrain.ResetState();
			for(int i = 0; i<BC.vision.numOutputs();i++){
				myBrain.InputSignalArray[i] = BC.vision.getOutputValue(i);
			}
			myBrain.Activate();

			float outPut = (float)myBrain.OutputSignalArray[0];
			if(outPut > 0.5f){
				BC.Flap();
			}
		}else{
			Debug.LogError("Bird "+BC.ID+" does not have a brain");
		}

		/*
		if(BC.vision.getOutputValue(0) < 2f && BC.vision.getOutputValue(2) > 0.5f){
		
			float chance = Random.Range(0,100);
			if(chance < 5){
				BC.Flap();
			}
			if(BC.vision.getOutputValue(0) < -3f || BC.vision.getOutputValue(2) > -0.5f){
				BC.Flap();
			}
		}*/
	}
}
