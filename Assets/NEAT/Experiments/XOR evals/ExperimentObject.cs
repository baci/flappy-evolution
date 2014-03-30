using UnityEngine;
using System.Collections;
using System.Xml;

public class ExperimentObject : MonoBehaviour {

	XORexperiment experiment = new XORexperiment();
	public XmlElement xml; //TODO: define this

	void Start () {
		Debug.LogError("Define the XML, pls!");
		return;
		experiment.Initialize("any name", xml);
	}
}
