using TMPro;
using UnityEngine;

public class NumberInputValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

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
        string validText = "";
        int dotCount = 0;

        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                validText += c;
            }
            else if (c == '.' && dotCount == 0)
            {
                validText += c;
                dotCount++;
            }
        }

        if (validText.StartsWith("."))
        {
            validText = validText.Substring(1); // Удаляем точку в начале
        }

        _inputField.text = validText;
    }
}