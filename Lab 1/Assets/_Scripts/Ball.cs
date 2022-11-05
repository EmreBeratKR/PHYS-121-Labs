using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fill, outline;
    [SerializeField] private Color fillColor, outlineColor;
    [SerializeField, Min(0f)] private float radius, outlineThickness;


    public Vector3 Position
    {
        get => transform.position - Vector3.down * radius;
        set => transform.position = value + Vector3.up * radius;
    }
    

    private float Diameter => radius * 2f;


#if UNITY_EDITOR

    private void OnValidate()
    {
        Validate_Shape();
    }


    private void Validate_Shape()
    {
        outlineThickness = Mathf.Clamp(outlineThickness, 0f, Diameter);
        
        fill.color = fillColor;
        outline.color = outlineColor;

        outline.transform.localScale = Vector3.one * Diameter;
        fill.transform.localScale = Vector3.one * (Diameter - outlineThickness);
    }

#endif
}
