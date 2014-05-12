using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("GenerationData")]
public class GenerationData
{
	[XmlArray("Data")]
	[XmlArrayItem("Generation")]
	public List<EvolutionStats.generation> data;


	public GenerationData()
	{
		data = new List<EvolutionStats.generation>();
	}

	public static GenerationData LoadFromFile(string path)
	{
		var serializer = new XmlSerializer(typeof(GenerationData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as GenerationData;
		}
	}

	public void SaveToFile(string path)
	{
		var serializer = new XmlSerializer(typeof(GenerationData));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
}

