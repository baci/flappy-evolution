using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using SharpNeat.Decoders;
using SharpNeat.Core;
using SharpNeat.EvolutionAlgorithms.ComplexityRegulation;
using SharpNeat.Decoders.Neat;
using SharpNeat.Phenomes;
using SharpNeat.DistanceMetrics;
using SharpNeat.SpeciationStrategies;
using System.Xml;

/// <summary>
/// Shitty functions. Nothing here is organized. I'm just copying things I need.
/// </summary>
public class ShittyFunctions {

	/// From: SharpNeat.Domains.ExperimentUtils.cs
	/// <summary>
	/// Create a network activation scheme from the scheme setting in the provided config XML.
	/// </summary>
	/// <returns></returns>
	public static NetworkActivationScheme CreateActivationScheme(XmlElement xmlConfig, string activationElemName)
	{
		// Get root activation element.
		XmlNodeList nodeList = xmlConfig.GetElementsByTagName(activationElemName, "");
		if(nodeList.Count != 1) {
			throw new ArgumentException("Missing or invalid activation XML config setting.");
		}
		
		XmlElement xmlActivation = nodeList[0] as XmlElement;
		string schemeStr = XmlUtils.TryGetValueAsString(xmlActivation, "Scheme");
		switch(schemeStr)
		{
		case "Acyclic":
			return NetworkActivationScheme.CreateAcyclicScheme();
		case "CyclicFixedIters":
			int iters = XmlUtils.GetValueAsInt(xmlActivation, "Iters");
			return NetworkActivationScheme.CreateCyclicFixedTimestepsScheme(iters);
		case "CyclicRelax":
			double deltaThreshold = XmlUtils.GetValueAsDouble(xmlActivation, "Threshold");
			int maxIters = XmlUtils.GetValueAsInt(xmlActivation, "MaxIters");
			return NetworkActivationScheme.CreateCyclicRelaxingActivationScheme(deltaThreshold, maxIters);
		}
		throw new ArgumentException(string.Format("Invalid or missing ActivationScheme XML config setting [{0}]", schemeStr));
	}
}
