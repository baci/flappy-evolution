﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using SharpNeat.Core;
using SharpNeat.Genomes.Neat;

public class gameController : MonoBehaviour {

	public IGenomeDecoder<NeatGenome, IBlackBox> genomeDecoder;

	public float gravity;
	public float forwardSpeed;

	public int generation = 0;

	//public int numBirds;

	public float spawnDistanceArea;

	public GameObject birdPre;

	public int birdsAlive;

	public float maxDistance;

	GameObject birdHolder;

	public List<IBlackBox> allBrains = new List<IBlackBox>();
	public List<NeatGenome> allGenomes = new List<NeatGenome>();

	public List<birdController> allBirds = new List<birdController>();

	public static gameController instance;

	//public Color[] speciesColor;

	public Material defaultMat;
	public Material[] birdMats;

	public int[] numSpeciseLeft;
	public int speciesLeft;

	public bool _isSimulating = false;
	public bool isSimulating {
		get{ return _isSimulating;}
		set{
			_isSimulating = value;
			Debug.Log("isSimulating changed to " + value);
		}
	}

	//[Range(0,1000)]
	public float framesPerSecond = 30;

	public float movementMulti = 1;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//Application.targetFrameRate = -1;
		Application.targetFrameRate = 30;
			
		Physics2D.gravity = new Vector2(0,-gravity);

		birdHolder = new GameObject();
		birdHolder.name = "birdHolder";

		birdStatistics.instance.Init(EvolutionSettings.instance.PopulationSize);

		birdMats = new Material[EvolutionSettings.instance.PopulationSize];
		//speciesColor = new Color[EvolutionSettings.instance.PopulationSize];
		for(int i= 0;i<birdMats.Length;i++){
			birdMats[i] = new Material(defaultMat);
			birdMats[i].color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),0.6f);
		}

		for(int i=0;i< EvolutionSettings.instance.PopulationSize;i++){
			GameObject g = Instantiate(birdPre) as GameObject;
			g.transform.parent = birdHolder.transform;
			birdController bc = g.GetComponent<birdController>();
			bc.ID = i;
			setBirdStats(bc);
			allBirds.Add(bc);
		}

		//newRound();
	}


	public void ResetRound(List<NeatGenome> genomeList){
		newRound();

		List<IBlackBox> _blackBoxes = new List<IBlackBox>();
		foreach (NeatGenome genome in genomeList)
		{
			genome.CachedPhenome = genomeDecoder.Decode(genome);
			_blackBoxes.Add(genome.CachedPhenome as IBlackBox);
		}

		allGenomes = genomeList;
		allBrains = _blackBoxes;

		for(int i=0;i< Mathf.Min(allBirds.Count,allBrains.Count);i++){
			if(allBirds[i] != null && allBrains[i] != null){
				allBirds[i].AI.myBrain = allBrains[i];
				allBirds[i].AI.myBrain.BrainID = i;
				allBirds[i].speciesID = allGenomes[i].SpecieIdx;
				(genomeList[i].CachedPhenome as IBlackBox).BrainID = i;
	//			print("Setting brain ID "+i);
				if(allBrains[i]== null){
					Debug.LogError("Brain "+i+" is invalid");
				}
				allBirds[i].renderer.material = birdMats[allGenomes[i].SpecieIdx];
			}else{
				Debug.LogError("No bird for the brain");
			}
		}
		print("number of birds "+allBirds.Count+" number of brainz "+allBrains.Count);		

		CalculateSpeciesNum();

		isSimulating = true;
	}

	void CalculateSpeciesNum(){
		numSpeciseLeft = new int[EvolutionSettings.instance.SpecieCount];
		for(int i=0;i<allBirds.Count;i++){
			numSpeciseLeft[allBirds[i].speciesID] ++;
		}
	}

	void newRound(){
		generation++;
		print("starting generation "+generation);


		Vector3 camPos = Camera.main.transform.position;
		camPos.x = 0;
		Camera.main.transform.position = camPos;

		pipeGenerator.instance.Clear();
		pipeGenerator.instance.GenerateStart();

		birdStatistics.instance.Clear();
		birdsAlive = EvolutionSettings.instance.PopulationSize;

		foreach(birdController bc in allBirds){
			ResetBird(bc);
			setBirdStats(bc);
		}

	}

	void ResetBird(birdController bc){
		if(bc.vision){
			bc.vision.enabled = true;
		}
		float xStart = 0;
		bc.transform.position = new Vector3(xStart,0,0);
		bc.dead = false;
		bc.velocity *= 0;
		bc.points = 0;
		bc.enabled = true;
		bc.birdRuntime = new birdController.BirdRuntime();

	}

	void setBirdStats(birdController bc){
		bc.birdStats.vision = 0.0f;



	}

	// Update is called once per frame
	void Update () {

		speciesLeft = 0;
		foreach(int i in numSpeciseLeft){
			if(i > 0){
				speciesLeft++;
			}
		}

		Application.targetFrameRate = (int)framesPerSecond;

		if(!isSimulating)return;

		if(birdsAlive == 0){
			isSimulating = false;
			EvolutionStats.instance.AnalyzeStats();

			FlappyExperimentObject._ea.currentStatus = HaxorsEvolutionAlgorithm<NeatGenome>.SimulationStatus.ENDING;
			FlappyExperimentObject._ea.FinishGeneration();
			FlappyExperimentObject._ea.FinishGeneration();
			print("All birds are dead, end of simulation");
			//newRound();
			//TODO: tell something that the generation is done
		}
	}

	void OnGUI(){
		framesPerSecond = GUI.HorizontalSlider(new Rect(0,0,250,500),framesPerSecond,0,1000);
	}
}
