using TMPro;
using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField] private TMP_Text titleField;
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform thickLines, tinyLines;
    [SerializeField] private string title;
    [SerializeField] private float minValueAngle, maxValueAngle;
    [SerializeField] private float minValue, maxValue;
    [SerializeField, Range(0f, 1f)] private float value01;


    public void UpdateValue(float value)
    {
        value01 = (value - minValue) / (maxValue - minValue);
        pointer.eulerAngles = Vector3.forward * GetAngle(value01);
    }
    

    private float GetAngle(float t)
    {
        return Mathf.Lerp(minValueAngle, maxValueAngle, t);
    }
    


#if UNITY_EDITOR

    private void OnValidate()
    {
        Validate_Lines();
        Validate_Title();
        Validate_Pointer();
    }


    private void Validate_Lines()
    {
        for (int i = 0; i < thickLines.childCount; i++)
        {
            var child = thickLines.GetChild(i);
            var t = (float) i / (thickLines.childCount - 1);
            child.eulerAngles = Vector3.forward * GetAngle(t);

            var textField = child.GetComponentInChildren<TextMeshPro>();
            textField.transform.eulerAngles = Vector3.zero;
            textField.text = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, t)).ToString();
        }

        for (int i = 0; i < thickLines.childCount - 1; i++)
        {
            var subCount = tinyLines.childCount / (thickLines.childCount - 1);
            for (int j = 0; j < subCount; j++)
            {
                var t1 = (float) i / (thickLines.childCount - 1);
                var t2 = ((float) i + 1) / (thickLines.childCount - 1);
                var thickChildAngle = GetAngle(t1);
                var nextThickChildAngle = GetAngle(t2);
                var child = tinyLines.GetChild(i * subCount + j);
                var t = ((float) j + 1) / (subCount + 1);
                child.eulerAngles = Vector3.forward * Mathf.Lerp(thickChildAngle, nextThickChildAngle, t);
            }
        }
    }

    private void Validate_Title()
    {
        titleField.text = title;
    }

    private void Validate_Pointer()
    {
        pointer.eulerAngles = Vector3.forward * GetAngle(value01);
    }

#endif
}
