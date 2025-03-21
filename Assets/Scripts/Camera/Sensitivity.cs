using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    [SerializeField] private float _dragSpeed = 0.01f;
    [SerializeField] private Slider _sliderSensitivity;

    private float _sensitivityValue = 0.01f;

    public float SensitivityValue => _sensitivityValue;

    private void OnEnable()
    {
        _sliderSensitivity.onValueChanged.AddListener(ChangeSensitivity);
    }

    private void OnDisable()
    {
        _sliderSensitivity.onValueChanged.RemoveListener(ChangeSensitivity);
    }

    private void ChangeSensitivity(float value)
    {
        _sensitivityValue = _dragSpeed * value;
    }
}
