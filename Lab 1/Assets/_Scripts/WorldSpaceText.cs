using TMPro;
using UnityEngine;

public class WorldSpaceText : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;


    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public Vector3 EulerAngles
    {
        get => transform.eulerAngles;
        set => transform.eulerAngles = value;
    }
    

    public void SetText(object obj)
    {
        textField.text = obj.ToString();
    }
}
