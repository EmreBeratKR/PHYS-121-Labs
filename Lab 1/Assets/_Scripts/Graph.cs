using UnityEngine;

public class Graph : MonoBehaviour
{
    private const float BaseGraphScale = 0.1f;


    [SerializeField] private WorldSpaceText worldSpaceTextPrefab;
    [SerializeField] private Transform linePrefab;
    [SerializeField] private Simulator simulator;
    [SerializeField] private Transform horizontalLines, verticalLines;
    [SerializeField] private Transform horizontalNumbers, verticalNumbers;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Transform xAxisTransform, yAxisTransform;
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3Int gridSize;
    [SerializeField, Min(0f)] private float subLineLength = 0.1f;
    [SerializeField, Min(0f)] private float subLineThickness = 0.75f;


    private Vector3 GraphOrigin => originTransform.position;
    private float GraphAntiScale => 1f / m_GraphScale;


    private float m_GraphScale = BaseGraphScale;
    

    private void Start()
    {
        ConstructLines();
    }


    public Vector3 TransformPoint(Vector3 position)
    {
        return GraphOrigin + position * m_GraphScale;
    }

    public void SetScale(float scaleMultiplier)
    {
        m_GraphScale = BaseGraphScale * scaleMultiplier;
        OnBecomeDirty();
    }
    

    private void OnBecomeDirty()
    {
        ConstructLines();
        simulator.ForceSetDirty();
    }

    private void ConstructLines()
    {
        const int gapCount = 5;
        
        
        horizontalLines.DestroyAllChildren();
        verticalLines.DestroyAllChildren();
        
        horizontalNumbers.DestroyAllChildren();
        verticalNumbers.DestroyAllChildren();

        var gridEdgeX = size.x / gridSize.x;
        var gridEdgeY = size.y / gridSize.y;
        
        for (int i = 0; i < gridSize.y; i++)
        {
            var point = GraphOrigin + Vector3.up * (gridEdgeY * (i + 1));
            var line = Instantiate(linePrefab, horizontalLines);
            line.position = point;
            line.localEulerAngles = Vector3.zero;
            line.localScale = new Vector3(size.x, 1f, 1f);

            var number = Instantiate(worldSpaceTextPrefab, horizontalNumbers);
            number.Position = point + Vector3.left * 0.3f;
            var value = (size.y / gridSize.y) * (i + 1) * GraphAntiScale;
            number.SetText(value);
        }

        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j <= gapCount; j++)
            {
                var point = GraphOrigin + Vector3.up * (gridEdgeY * (i + ((float) j / gapCount)));
                var line = Instantiate(linePrefab, horizontalLines);
                line.position = point + Vector3.left * (subLineLength * 0.5f);
                line.localEulerAngles = Vector3.zero;
                line.localScale = new Vector3(subLineLength, subLineThickness, 1f);
            }
        }

        for (int i = 0; i < gridSize.x; i++)
        {
            var point = GraphOrigin + Vector3.right * (gridEdgeX * (i + 1));
            var line = Instantiate(linePrefab, verticalLines);
            line.position = point;
            line.localEulerAngles = Vector3.zero;
            line.localScale = new Vector3(size.y, 1f, 1f);
            
            var number = Instantiate(worldSpaceTextPrefab, verticalNumbers);
            number.Position = point + Vector3.down * 0.2f;
            number.EulerAngles = Vector3.zero;
            var value = (size.x / gridSize.x) * (i + 1) * GraphAntiScale;
            number.SetText(value);
        }
        
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j <= gapCount; j++)
            {
                var point = GraphOrigin + Vector3.right * (gridEdgeX * (i + ((float) j / gapCount)));
                var line = Instantiate(linePrefab, verticalLines);
                line.position = point + Vector3.down * (subLineLength * 0.5f);
                line.localEulerAngles = Vector3.zero;
                line.localScale = new Vector3(subLineLength, subLineThickness, 1f);
            }
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