using UnityEngine;
using System.Collections;

public class graphCreater : MonoBehaviour {

	EvolutionStats ES; 

	public LineRenderer[] graphs;

	public Vector2 graphSize;

	public static graphCreater instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		ES = EvolutionStats.instance;
	}
	
	// Update is called once per frame
	public void UpdateGraph () {
		Grapth1();
		Grapth2();
		Grapth3();
	}

	//best bird
	void Grapth1(){
		graphs[0].SetVertexCount(ES.topLengths.Count);
		
		float maxHeight = Mathf.Max(ES.topLengths.ToArray());
		
		float multiY = graphSize.y/maxHeight;
		float multiX = Mathf.Min(1, graphSize.x/ES.topLengths.Count);
				
		for(int i=0;i<ES.topLengths.Count;i++){
			graphs[0].SetPosition(i,new Vector3(i*multiX,ES.topLengths[i]*multiY,0));
		}
	}

	//worst bird
	void Grapth2(){
		graphs[1].SetVertexCount(ES.topLengths.Count);
		
		float maxHeight = Mathf.Max(ES.topLengths.ToArray());
		
		float multiY = graphSize.y/maxHeight;
		float multiX = Mathf.Min(1, graphSize.x/ES.topLengths.Count);
		
		for(int i=0;i<ES.lowLengths.Count;i++){
			graphs[1].SetPosition(i,new Vector3(i*multiX,ES.lowLengths[i]*multiY,0));
		}
	}

	//avg bird
	void Grapth3(){
		graphs[2].SetVertexCount(ES.topLengths.Count);
		
		float maxHeight = Mathf.Max(ES.topLengths.ToArray());
		
		float multiY = graphSize.y/maxHeight;
		float multiX = Mathf.Min(1, graphSize.x/ES.topLengths.Count);
		
		for(int i=0;i<ES.avgLengths.Count;i++){
			graphs[2].SetPosition(i,new Vector3(i*multiX,ES.avgLengths[i]*multiY,0));
		}
	}
}
