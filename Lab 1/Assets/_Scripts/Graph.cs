using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform originTransform;
    [SerializeField, Min(0f)] private float graphScale = 1f;
    
    
    private Vector3 GraphOrigin => originTransform.position;
    
    
    public Vector3 TransformPoint(Vector3 position)
    {
        return GraphOrigin + position * graphScale;
    }
}