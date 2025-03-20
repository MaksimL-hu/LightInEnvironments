using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private InputPanelForRefractiveIndex _panel;
    [SerializeField] private float _refraction;

    public float Refraction => _refraction;

    private void OnDisable()
    {
        _panel.InputField.onValueChanged.RemoveListener(SetRefractiveIndex);
    }

    private void SetRefractiveIndex(string value)
    {
        if(float.TryParse(value, out float result))
            _refraction = result;
    }

    public void BuildEnvironment(InputPanelForRefractiveIndex panel)
    {
        _panel = panel;
        _refraction = 1f;
        _panel.InputField.onValueChanged.AddListener(SetRefractiveIndex);
    }
}