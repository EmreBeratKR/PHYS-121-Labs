using UnityEngine;
using UnityEngine.Events;

public class Simulator : MonoBehaviour
{
    [SerializeField] private IndependentVariable speedVariable, angleVariable, heightVariable;
    [SerializeField] private Graph graph;
    [SerializeField] private Ball ball;
    [SerializeField] private Vector3 gravity = new Vector3(0f, -9.81f, 0f);


    public UnityAction<float> OnMoveNext;
    public UnityAction OnSimulationEnd;
    public UnityAction OnSimulationStart;
    public UnityAction OnSimulationStop;


    public bool IsSimulationEnd => IsSimulating && ElapsedTime01 >= 1f;
    public bool IsSimulating { get; private set; }
    
    
    private Vector3 Acceleration => gravity;
    private float InitialSpeed => speedVariable.Value;
    private float InitialAnglesInDegree => angleVariable.Value;
    private float InitialHeight => heightVariable.Value;
    private float ElapsedTime01 => m_ElapsedTime / m_TotalFlightTime;


    private Vector3 m_InitialPosition;
    private Vector3 m_InitialVelocity;
    private float m_TotalFlightTime;
    private float m_MaxHeight;
    private float m_ElapsedTime;
    private float m_SimulationSpeed = 1f;


    private void Update()
    {
        if (!IsSimulating) return;
        
        if (IsSimulationEnd) return;

        MoveNext();
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

        IsSimulating = true;
        
        OnSimulationStart?.Invoke();
    }

    public void PauseSimulating()
    {
        IsSimulating = false;
    }

    public void StopSimulating()
    {
        IsSimulating = false;
        UpdateElapsedTime(0f);
        
        OnSimulationStop?.Invoke();
    }

    public void UpdateElapsedTime(float t)
    {
        m_ElapsedTime = m_TotalFlightTime * t;
        OnBecomeDirty();
    }

    public void ForceSetDirty()
    {
        OnBecomeDirty();
    }

    public void SetSimulationSpeed(float speed)
    {
        m_SimulationSpeed = speed;
    }
    
    
    private void OnBecomeDirty()
    {
        UpdateSimulation();
    }

    private void UpdateSimulation()
    {
        var positionOverTime = GetPositionOverTime(m_ElapsedTime);
        ball.Position = graph.TransformPoint(positionOverTime);
    }

    private void MoveNext()
    {
        var scaledDeltaTime = Time.deltaTime * m_SimulationSpeed;
        m_ElapsedTime = Mathf.Clamp(m_ElapsedTime + scaledDeltaTime, 0f, m_TotalFlightTime);

        OnBecomeDirty();
        OnMoveNext?.Invoke(ElapsedTime01);
        
        if (!IsSimulationEnd) return;
        
        OnSimulationEnd?.Invoke();
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
        m_ElapsedTime = Mathf.Clamp(m_ElapsedTime, 0f, m_TotalFlightTime);
    }

#endif
}