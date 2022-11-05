using TMPro;
using UnityEngine;

public class IndependentVariable : MonoBehaviour
{
    [SerializeField] private TMP_Text nameField, valueField;
    [SerializeField] private string variableName, unit;
    [SerializeField] private float defaultValue;

    
    public float Value
    {
        get => m_Value;
        private set
        {
            m_Value = value;
            valueField.text = this.ToString();
        }
    }
    

    private float m_Value;


    private void Start()
    {
        Value = defaultValue;
    }


    public void IncreaseValue(float value)
    {
        Value += value;
    }

    public void DecreaseValue(float value)
    {
        Value -= value;
    }


    public override string ToString()
    {
        return $"{m_Value}{unit}";
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        nameField.text = variableName;
        valueField.text = this.ToString();
    }

#endif
}