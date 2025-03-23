using TMPro;
using UnityEngine;

public class NumberInputValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private char _validSymbol = ',';

    private void OnEnable()
    {
        _inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveListener(ValidateInput);
    }

    private void ValidateInput(string text)
    {
        string validText = string.Empty;
        int dotCount = 0;

        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                validText += c;
            }
            else if (c == _validSymbol && dotCount == 0)
            {
                validText += c;
                dotCount++;
            }
        }

        if (validText.StartsWith(_validSymbol))
        {
            validText = validText.Substring(1);
        }

        _inputField.text = validText;
    }
}