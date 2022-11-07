using UnityEngine;
using UnityEngine.UI;

public abstract class CustomSlider : MonoBehaviour
{
    protected Slider Slider
    {
        get
        {
            if (!m_Slider)
            {
                m_Slider = GetComponentInChildren<Slider>();
            }

            return m_Slider;
        }
    }


    private Slider m_Slider;


    protected virtual void Awake()
    {
        Slider.onValueChanged.AddListener(OnValueChanged);
    }


    protected void StartListening()
    {
        Slider.onValueChanged.AddListener(OnValueChanged);
    }

    protected void StopListening()
    {
        Slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    protected virtual void OnValueChanged(float value)
    {
        
    }
}
