using UnityEngine;
using System.Collections;

public class pipeController : MonoBehaviour {

	public GameObject topPipe;
	public GameObject buttomPipe;

	public PipeGap gap;

	pipeGenerator gen;

	// Use this for initialization
	void Start () {
		gen = pipeGenerator.instance;

		Vector3 scale = transform.localScale;
		scale.y = gen.gapSize -(gen.gapSizeDecrease*transform.position.x);
		scale.y = Mathf.Max(scale.y,pipeGenerator.instance.minGapSize);

		gap.transform.localScale = scale;

		Vector3 posTop = Vector3.zero;
		//Vector3 posButtom = Vector3.zero;

		posTop.y = topPipe.transform.localScale.y/2+scale.y/2;
		//posTop.y = Mathf.Min(posTop.y,topPipe.transform.localScale.y/2+transform.position.y);
		//posButtom.y = -buttomPipe.transform.localScale.y/2-scale.y/2;

		topPipe.transform.localPosition = posTop;
		buttomPipe.transform.localPosition = -posTop;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x+10 < Camera.main.transform.position.x){
			pipeGenerator.instance.generate();
			Destroy(gameObject);
		}
	}
}
