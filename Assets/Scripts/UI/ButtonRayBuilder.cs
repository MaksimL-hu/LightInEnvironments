using UnityEngine;
using UnityEngine.UI;

public class ButtonRayBuilder : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private PathBuilder _builder;

    private void OnEnable()
    {
        _button.onClick.AddListener(_builder.CalculatePath);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(_builder.CalculatePath);
    }
}