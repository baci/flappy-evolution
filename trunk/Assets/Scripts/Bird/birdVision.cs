using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class birdVision : MonoBehaviour {

	RaycastHit2D[] output;

	public LayerMask mask;

	public List<Vector2> raycastDirections = new List<Vector2>();
	public List<Vector2> raycastPositions = new List<Vector2>();
	public List<float> raycastRanges = new List<float>();

	Vector2 pos;

	float[] floatOutPuts;

	birdController BC;

	// Use this for initialization
	void Awake () {
		output = new RaycastHit2D[raycastDirections.Count];

		BC = GetComponent<birdController>();

		floatOutPuts = new float[8];
		EvolutionSettings.instance.numInputs = floatOutPuts.Length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float VA = BC.birdStats.vision;
		if(gameController.instance.isSimulating){

			//distance to top of world
			floatOutPuts[0] = 5-transform.position.y;
			//distance to bottom of world
			floatOutPuts[1] = -5-transform.position.y;
			//distance to closest gap
			floatOutPuts[2] = pipeGenerator.instance.closestGap.transform.position.x-transform.position.x;
			//floatOutPuts[3] = pipeGenerator.instance.closestGap.transform.position.y;

			//gap size
			floatOutPuts[3] = pipeGenerator.instance.closestGap.transform.localScale.y;
			//y speed of the bird
			floatOutPuts[4] = BC.velocity.y;

			//distance to gap top
			floatOutPuts[5] = pipeGenerator.instance.closestGap.transform.position.y-transform.position.y+pipeGenerator.instance.closestGap.transform.localScale.y;

			//distance to gap bottom
			floatOutPuts[6] = pipeGenerator.instance.closestGap.transform.position.y-transform.position.y-pipeGenerator.instance.closestGap.transform.localScale.y;

			floatOutPuts[7] = gameController.instance.gravity;

			for(int i=0;i<floatOutPuts.Length;i++){
				floatOutPuts[i] += floatOutPuts[i]*Random.Range(-VA,VA);
			}

		}
	}

	public float getOutputValue(int num){
		//return output[num].fraction;
		return floatOutPuts[num];
	}

	public int numOutputs(){
		return floatOutPuts.Length;
	}

	void OnDrawGizmos() {
		/*
		for(int i=0;i<raycastDirections.Count;i++){
			if(output[i].transform == null){
				Gizmos.color = Color.green;
			}else{
				Gizmos.color = Color.red;
			}
			Gizmos.DrawRay(pos+raycastPositions[i],raycastDirections[i]*raycastRanges[i]);
		}
		*/
	}
}
