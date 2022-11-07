using System;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Simulator simulator;
    [SerializeField] private CanvasGroup variableGroup;


    private void OnEnable()
    {
        if (simulator)
        {
            simulator.OnSimulationStart += OnSimulationStart;
            simulator.OnSimulationStop += OnSimulationStop;
        }
    }

    private void OnDisable()
    {
        if (simulator)
        {
            simulator.OnSimulationStart -= OnSimulationStart;
            simulator.OnSimulationStop -= OnSimulationStop;
        }
    }


    private void OnSimulationStart()
    {
        variableGroup.interactable = false;
    }

    private void OnSimulationStop()
    {
        variableGroup.interactable = true;  
    }
}