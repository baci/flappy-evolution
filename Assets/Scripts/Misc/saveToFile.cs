using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Reflection;
using System.IO;
//using Newtonsoft.Json;

public class saveToFile : MonoBehaviour {

	string filePath;

	public string fileName;

	public string textToWrite;

	// Use this for initialization
	void Start () {
		filePath = Application.dataPath+"/SavedData/";
		print(filePath);

		Directory.CreateDirectory(filePath);

		File.WriteAllText(filePath+fileName+".txt",textToWrite);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
