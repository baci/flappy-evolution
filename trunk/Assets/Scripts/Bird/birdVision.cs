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

		floatOutPuts = new float[5];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float VA = BC.birdStats.vision;
		if(gameController.instance.isSimulating){
			/*
			 * 0 = distance from roof
			 * 1 = distance from floor
			 * 2 = x distance to closest pipe
			 * 3 = y difference to gap center
			 * 4 = gap y size
			 * */
			floatOutPuts[0] = 5-transform.position.y;
			floatOutPuts[1] = -5-transform.position.y;
			floatOutPuts[2] = pipeGenerator.instance.closestGap.transform.position.x-transform.position.x;
			//floatOutPuts[3] = pipeGenerator.instance.closestGap.transform.position.y;
			floatOutPuts[3] = pipeGenerator.instance.closestGap.transform.position.y-transform.position.y;
			floatOutPuts[4] = pipeGenerator.instance.closestGap.transform.localScale.y;
		
			floatOutPuts[0] += floatOutPuts[0]*Random.Range(-VA,VA);
			floatOutPuts[1] += floatOutPuts[1]*Random.Range(-VA,VA);
			floatOutPuts[2] += floatOutPuts[2]*Random.Range(-VA,VA);
			floatOutPuts[3] += floatOutPuts[3]*Random.Range(-VA,VA);
			floatOutPuts[4] += floatOutPuts[4]*Random.Range(-VA,VA);

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
