using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Core;
using SharpNeat.Phenomes;

public class HaxorsEvolutionAlgorithm<TGenome> : NeatEvolutionAlgorithm<TGenome>
where TGenome : class, IGenome<TGenome>  {

	public delegate void ListEvent(List<TGenome> list);
	public static ListEvent OnGenerationStarted; //I don't know what this will do now?

	public enum SimulationStatus{
		WAITING_TO_START,
		RUNNING,
		ENDING
	}

	protected SimulationStatus _currentStatus = SimulationStatus.WAITING_TO_START;
	bool emptySpeciesFlag;
	int offspringCount;
	SpecieStats[] specieStatsArr;
	List<TGenome> offspringList;

	protected override void PerformOneGeneration ()
	{

		if(_currentStatus == SimulationStatus.WAITING_TO_START)
		{
			// Calculate statistics for each specie (mean fitness, target size, number of offspring to produce etc.)
			offspringCount = 0;
			specieStatsArr = CalcSpecieStats(out offspringCount);
			
			// Create offspring.
			offspringList = CreateOffspring(specieStatsArr, offspringCount);
			
			// Trim species back to their elite genomes.
			emptySpeciesFlag = TrimSpeciesBackToElite(specieStatsArr);
			
			// Rebuild _genomeList. It will now contain just the elite genomes.
			RebuildGenomeList();
			
			// Append offspring genomes to the elite genomes in _genomeList. We do this before calling the
			// _genomeListEvaluator.Evaluate because some evaluation schemes re-evaluate the elite genomes 
			// (otherwise we could just evaluate offspringList).
			_genomeList.AddRange(offspringList);
			//TODO: exit Waiting to Start mode and 

			if(OnGenerationStarted != null){
				OnGenerationStarted(offspringList);
				_currentStatus = SimulationStatus.RUNNING;
			}
			return;
		} else if (_currentStatus == SimulationStatus.RUNNING)
		{
			//TODO: Lock until all birds are dead!
			return;
		}

		//AKA if(_currentStatus == SimulationStatus.ENDING)
		// Evaluate genomes.
		_genomeListEvaluator.Evaluate(_genomeList);
		/**
		 * TODO: Vince says: This Evaluate is meant to check the fitness of a bird.
		 * We should most likely keep adding inputs at every physics tick and extracting
		 * the tap-or-no-tap.
		 * When the simulation is over, feed the distance back as fitness and
		 * let evolution do its job.
		 */

		// Integrate offspring into species.
		if(emptySpeciesFlag)
		{   
			// We have one or more terminated species. Therefore we need to fully re-speciate all genomes to divide them
			// evenly between the required number of species.
			
			// Clear all genomes from species (we still have the elite genomes in _genomeList).
			ClearAllSpecies();
			
			// Speciate genomeList.
			_speciationStrategy.SpeciateGenomes(_genomeList, _specieList);
		}
		else
		{
			// Integrate offspring into the existing species. 
			_speciationStrategy.SpeciateOffspring(offspringList, _specieList);            
		}
		//Debug.Assert(!TestForEmptySpecies(_specieList), "Speciation resulted in one or more empty species.");
		
		// Sort the genomes in each specie. Fittest first (secondary sort - youngest first).
		SortSpecieGenomes();
		
		// Update stats and store reference to best genome.
		UpdateBestGenome();
		UpdateStats();
		
		// Determine the complexity regulation mode and switch over to the appropriate set of evolution
		// algorithm parameters. Also notify the genome factory to allow it to modify how it creates genomes
		// (e.g. reduce or disable additive mutations).
		_complexityRegulationMode = _complexityRegulationStrategy.DetermineMode(_stats);
		_genomeFactory.SearchMode = (int)_complexityRegulationMode;
		switch((int)_complexityRegulationMode)
		{
		case /*ComplexityRegulationMode.Complexifying*/0:
			_eaParams = _eaParamsComplexifying;
			break;
		case /*ComplexityRegulationMode.Simplifying*/1:
			_eaParams = _eaParamsSimplifying;
			break;
		}
		
		// TODO: More checks.
		//Debug.Assert(_genomeList.Count == _populationSize)
	}
}
