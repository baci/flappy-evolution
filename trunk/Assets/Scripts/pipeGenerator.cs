using UnityEngine;
using System.Collections;

public class pipeGenerator : MonoBehaviour {

	public GameObject pipePre;


	public static pipeGenerator instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		for(int i=0;i<4;i++){
			Instantiate(pipePre, new Vector3(i*10f+10,Random.Range(-2f,2f),0),Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generate(){
		Instantiate(pipePre, new Vector3(Camera.main.transform.position.x+30,Random.Range(-2f,2f),0),Quaternion.identity);
	}
}
