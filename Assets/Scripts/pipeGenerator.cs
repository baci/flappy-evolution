using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pipeGenerator : MonoBehaviour {

	public GameObject pipePre;

	public GameObject closestGap;

	GameObject pipeHolder;

	List<GameObject> pipes = new List<GameObject>();

	public static pipeGenerator instance;
	void Awake(){
		instance = this;
		pipeHolder = new GameObject();
		pipeHolder.name = "pipeHolder";
	}

	// Use this for initialization
	void Start () {

	}

	public void GenerateStart(){
		for(int i=0;i<4;i++){
			pipes.Add(Instantiate(pipePre, new Vector3(i*10f+10,Random.Range(-2f,2f),0),Quaternion.identity) as GameObject);
			pipes[pipes.Count-1].transform.parent = pipeHolder.transform;
		}
		FindClosestGap();
	}
	
	// Update is called once per frame
	void Update () {
		FindClosestGap();
	}

	void FindClosestGap(){
		float dis = Mathf.Infinity;
		GameObject gp = null;
		
		foreach(GameObject g in pipes){
			if(g != null){
				PipeGap gap = g.GetComponentInChildren<PipeGap>();
				float thisDis = gap.transform.position.x-Camera.main.transform.position.x;
				if(thisDis < dis && thisDis > -1){
					dis = thisDis;
					gp = gap.gameObject;
				}
			}
		}
		closestGap = gp;
	}

	public void Clear(){
		foreach(GameObject p in pipes){
			if(p != null){
				Destroy(p);
			}
		}
		pipes.Clear();
	}

	public void generate(){
		pipes.Add(Instantiate(pipePre, new Vector3(Camera.main.transform.position.x+30,Random.Range(-2f,2f),0),Quaternion.identity) as GameObject);
	}
}
