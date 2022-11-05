using UnityEngine;

public class Simulator : MonoBehaviour
{
    [SerializeField] private IndependentVariable speedVariable, angleVariable, heightVariable;
    [SerializeField] private Graph graph;
    [SerializeField] private Ball ball;
    [SerializeField] private Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    [SerializeField] private float elapsedTime;

    
    private Vector3 Acceleration => gravity;
    private float InitialSpeed => speedVariable.Value;
    private float InitialAnglesInDegree => angleVariable.Value;
    private float InitialHeight => heightVariable.Value;


    private Vector3 m_InitialPosition;
    private Vector3 m_InitialVelocity;
    private float m_TotalFlightTime;
    private float m_MaxHeight;
    private bool m_IsSimulating;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_IsSimulating)
            {
                StopSimulating();
            }

            else
            {
                StartSimulating();
            }
        }
        
        if (!m_IsSimulating) return;

        var positionOverTime = GetPositionOverTime(elapsedTime);
        ball.Position = graph.TransformPoint(positionOverTime); 
    }


    public void StartSimulating()
    {
        m_InitialPosition = new Vector3(0f, InitialHeight, 0f);
        
        var anglesInRadians = InitialAnglesInDegree * Mathf.Deg2Rad;
        var initialVelocityX = InitialSpeed * Mathf.Cos(anglesInRadians);
        var initialVelocityY = InitialSpeed * Mathf.Sin(anglesInRadians);
        m_InitialVelocity = new Vector3(initialVelocityX, initialVelocityY, 0f);
        
        m_TotalFlightTime = GetTotalFlightTime();
        m_MaxHeight = GetMaxHeight();

        m_IsSimulating = true;
    }

    public void StopSimulating()
    {
        m_IsSimulating = false;
    }
    
    
    private float GetTotalFlightTime()
    {
        var a = 0.5f * Acceleration.y;
        var b = m_InitialVelocity.y;
        var c = m_InitialPosition.y;
        var delta = b * b - 4 * a * c;
        return (-b - Mathf.Sqrt(delta)) / (2 * a);
    }

    private float GetMaxHeight()
    {
        return (-1f * m_InitialVelocity.y * m_InitialVelocity.y) / (2f * Acceleration.y);
    }

    private Vector3 GetPositionOverTime(float time)
    {
        return m_InitialPosition + m_InitialVelocity * time + Acceleration * (0.5f * time * time);
    }

    private Vector3 GetVelocityOverTime(float time)
    {
        return m_InitialVelocity + Acceleration * time;
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, m_TotalFlightTime);
    }

#endif
}