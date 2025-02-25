﻿using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Text;
using System.IO;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;

public class ExperimentObject : MonoBehaviour {

	private XORexperiment experiment = new XORexperiment();
	private static NeatEvolutionAlgorithm<NeatGenome> _ea;
	private XmlElement xml;

	private void Start () {
		XmlDocument xmlDoc = new XmlDocument();	
		string filepath = Application.dataPath+"/XMLAssets/"+"xor-config.xml";
		
		if(File.Exists (filepath)) {
			Debug.Log("loading XML config");

			xmlDoc.Load(filepath); //Load the XML file
			xml = xmlDoc.DocumentElement;

			experiment.Initialize("any name", xml);

			// Create evolution algorithm and attach update event.
			_ea = experiment.CreateEvolutionAlgorithm();
			_ea.UpdateEvent += new EventHandler(ea_UpdateEvent);
			
			// Start algorithm (it will run on a background thread).
			_ea.StartContinue();
		}
		else
			Debug.LogError("xml file not found: " + filepath);

	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(_ea != null)
				_ea.Stop();
			Application.Quit();
		}
	}

	void OnApplicationQuit(){
		if(_ea != null)
			_ea.Stop();

	}

	private static void ea_UpdateEvent(object sender, EventArgs e)
	{
		Debug.Log(string.Format("gen={0:N0} bestFitness={1:N6} meanFitness={2:N6} champComplexity={3:N3}",
		                        _ea.CurrentGeneration, _ea.Statistics._maxFitness, _ea.Statistics._meanFitness, _ea.CurrentChampGenome.Complexity));
		
		/*// Save the best genome to file
		var doc = NeatGenomeXmlIO.SaveComplete(
			new List<NeatGenome>() {_ea.CurrentChampGenome}, 
		false);
		doc.Save(CHAMPION_FILE);*/
	}
}
