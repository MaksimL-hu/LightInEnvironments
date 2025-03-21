using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToTextRenderer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        SetText(_slider.value);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetText);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetText);
    }

    private void SetText(float value)
    {
        _text.text = value.ToString();
    }
}