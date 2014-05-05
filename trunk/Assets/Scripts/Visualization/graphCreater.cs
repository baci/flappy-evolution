using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class graphCreater : MonoBehaviour {

	EvolutionStats ES; 

	public LineRenderer[] graphs;

	List<LineRenderer> speciesGraphs = new List<LineRenderer>();

	public Material defaultMat;

	public Vector2 graphSize;

	public GameObject speciesParent;

	public static graphCreater instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		ES = EvolutionStats.instance;
	}

	public void CreateSpeciesGraphsGraphObjects(){
		for(int i=0;i<EvolutionSettings.instance.SpecieCount;i++){
			GameObject g = new GameObject();
			g.layer = 10;
			g.name = "speciesGraph"+i;

			LineRenderer r= g.AddComponent<LineRenderer>();
			r.SetWidth(0.03f,0.03f);
			r.useWorldSpace = false;
			g.renderer.material = defaultMat;
			Color c = gameController.instance.birdMats[i].color;
			c.a = 1;
			g.renderer.material.color = c;
			
			g.transform.parent = speciesParent.transform;
			g.transform.position = speciesParent.transform.position;
			speciesGraphs.Add(r);
		}
	}

	void speciesGraphsUpdate(){
		float globalMax = 0;
		for(int i=0;i<EvolutionSettings.instance.SpecieCount;i++){
//			print(ES.speciesLengths.Count+"   "+i);
			speciesGraphs[i].SetVertexCount(ES.speciesLengths[i].Count);
		
			float maxHeight = Mathf.Max(ES.speciesLengths[i].ToArray());
			globalMax = Mathf.Max(maxHeight,globalMax);

			//print("MULTI Y IS "+maxHeight);
			float multiY = graphSize.y/globalMax;
			float multiX = Mathf.Min(1, graphSize.x/ES.speciesLengths[i].Count);
			
			for(int j=0;j<ES.speciesLengths[i].Count;j++){
				speciesGraphs[i].SetPosition(j,new Vector3(j*multiX,ES.speciesLengths[i][j]*multiY,0));
			}
		}
	}

	// Update is called once per frame
	public void UpdateGraph () {
		Grapth1();
		Grapth2();
		Grapth3();
		speciesGraphsUpdate();
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
