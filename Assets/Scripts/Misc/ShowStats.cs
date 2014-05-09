using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

	TextMesh mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = "Length "+(int)transform.position.x+"         FPS "+(int)(1/Time.deltaTime)+"\n"+
			" Round "+(gameController.instance.round+1)+"/"+(EvolutionSettings.instance.RoundPerGeneration+1)+" In Generation "+gameController.instance.generation+"\n"+
				" Birds alive "+gameController.instance.birdsActive+"/"+EvolutionSettings.instance.PopulationSize+"    Species Left "+gameController.instance.speciesLeft+"/"+EvolutionSettings.instance.SpecieCount+"\n";
	}
}
