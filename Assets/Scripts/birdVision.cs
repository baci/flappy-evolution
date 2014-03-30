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

	// Use this for initialization
	void Start () {
		output = new RaycastHit2D[raycastDirections.Count];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for(int i=0;i<raycastDirections.Count;i++){
			pos = new Vector2(transform.position.x,transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(pos +raycastPositions[i],raycastDirections[i] , raycastRanges[i], mask);

			output[i] = hit;
		}
	}

	public float getOutputValue(int num){
		return output[num].fraction;
	}

	void OnDrawGizmos() {
		for(int i=0;i<raycastDirections.Count;i++){
			if(output[i].transform == null){
				Gizmos.color = Color.green;
			}else{
				Gizmos.color = Color.red;
			}
			Gizmos.DrawRay(pos+raycastPositions[i],raycastDirections[i]*raycastRanges[i]);
		}
	}
}
