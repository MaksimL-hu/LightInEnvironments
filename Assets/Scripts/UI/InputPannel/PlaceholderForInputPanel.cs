using TMPro;
using UnityEngine;

public class PlaceholderForInputPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPlaceholder;
    [SerializeField] private TMP_InputField _inputField;

    private void OnEnable()
    {
        _inputField.onValueChanged.AddListener(ChangeTextPlaceholder);
    }

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveListener(ChangeTextPlaceholder);
    }

    private void ChangeTextPlaceholder(string value)
    {
        if (value == string.Empty)
            return;

        _textPlaceholder.text = value;
    }
}