using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphScaleSlider : CustomSlider
{
    [SerializeField] private Graph graph;
    [SerializeField] private TMP_Text valueField;


    private static readonly Dictionary<int, float> ValueDictionary = new Dictionary<int, float>()
    {
        {0, 0.25f},
        {1, 0.5f},
        {2, 1f},
        {3, 2f},
        {4, 4f}
    };


    protected override void OnValueChanged(float value)
    {
        var valuePair = ValueDictionary[(int) value];
        valueField.text = $"Zoom - {valuePair}x";
        graph.SetScale(valuePair);
    }
}
