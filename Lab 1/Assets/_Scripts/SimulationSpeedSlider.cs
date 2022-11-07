using TMPro;
using UnityEngine;

public class SimulationSpeedSlider : CustomSlider
{
    [SerializeField] private Simulator simulator;
    [SerializeField] private TMP_Text valueField;


    protected override void OnValueChanged(float value)
    {
        valueField.text = $"Simulation Speed - {value:0.0}x";
        simulator.SetSimulationSpeed(value);
    }
}