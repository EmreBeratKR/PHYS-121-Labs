using UnityEngine;

public class TimeLineSlider : CustomSlider
{
    [SerializeField] private Simulator simulator;
    [SerializeField] private GameObject playIcon, pauseIcon;


    private void OnEnable()
    {
        simulator.OnMoveNext += OnSimulationMoveNext;
        simulator.OnSimulationEnd += OnSimulationEnd;
    }

    private void OnDisable()
    {
        simulator.OnMoveNext -= OnSimulationMoveNext;
        simulator.OnSimulationEnd -= OnSimulationEnd;
    }


    protected override void OnValueChanged(float value)
    {
        UpdatePlayPauseButton(false);
        simulator.PauseSimulating();
        simulator.UpdateElapsedTime(value);
    }


    public void OnClickPlayPauseButton()
    {
        if (simulator.IsSimulationEnd)
        {
            UpdatePlayPauseButton(true);
            simulator.StopSimulating();
            simulator.StartSimulating();
            return;
        }
        
        if (simulator.IsSimulating)
        {
            UpdatePlayPauseButton(false);
            simulator.PauseSimulating();
            return;
        }
        
        UpdatePlayPauseButton(true);
        simulator.StartSimulating();
    }

    public void OnClickStopButton()
    {
        Slider.value = 0f;
        UpdatePlayPauseButton(false);
        simulator.StopSimulating();
    }


    private void OnSimulationMoveNext(float elapsedTime)
    {
        StopListening();
        Slider.value = elapsedTime;
        StartListening();
    }

    private void OnSimulationEnd()
    {
        UpdatePlayPauseButton(false);
    }
    

    private void UpdatePlayPauseButton(bool isPlaying)
    {
        playIcon.SetActive(!isPlaying);
        pauseIcon.SetActive(isPlaying);
    }
}
