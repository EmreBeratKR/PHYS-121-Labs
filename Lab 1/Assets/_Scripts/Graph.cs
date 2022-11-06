using System;
using UnityEditor;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform linePrefab;
    [SerializeField] private Transform horizontalLines, verticalLines;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Transform xAxisTransform, yAxisTransform;
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3Int gridSize;
    [SerializeField, Min(0f)] private float graphScale = 1f;


    private Vector3 GraphOrigin => originTransform.position;


    private void Update()
    {
        ConstructLines();
    }


    public Vector3 TransformPoint(Vector3 position)
    {
        return GraphOrigin + position * graphScale;
    }


    private void ConstructLines()
    {
        horizontalLines.DestroyAllChildren();
        verticalLines.DestroyAllChildren();

        var gridEdgeX = size.x / gridSize.x;
        var gridEdgeY = size.y / gridSize.y;
        
        for (int i = 0; i < gridSize.y; i++)
        {
            var point = GraphOrigin + Vector3.up * (gridEdgeY * (i + 1));
            var line = Instantiate(linePrefab, horizontalLines);
            line.position = point;
            line.localEulerAngles = Vector3.zero;
            line.localScale = new Vector3(size.x, 1f, 1f);
        }

        for (int i = 0; i < gridSize.x; i++)
        {
            var point = GraphOrigin + Vector3.right * (gridEdgeX * (i + 1));
            var line = Instantiate(linePrefab, verticalLines);
            line.position = point;
            line.localEulerAngles = Vector3.zero;
            line.localScale = new Vector3(size.y, 1f, 1f);
        }
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        Validate_Graph();
    }


    private void Validate_Graph()
    {
        xAxisTransform.localScale = new Vector3(size.x, 1f, 1f);
        yAxisTransform.localScale = new Vector3(size.y, 1f, 1f);
    }

#endif
}