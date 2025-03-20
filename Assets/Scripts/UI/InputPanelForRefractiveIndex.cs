using TMPro;
using UnityEngine;

public class InputPanelForRefractiveIndex : MonoBehaviour
{
    [SerializeField] private string _text;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private int _index;
    [SerializeField] private TMP_InputField _inputField;

    public int Index => _index;
    public TMP_InputField InputField => _inputField;

    public void SetIndex(int index)
    {
        _index = index;
        _textMeshPro.text = _text + _index + ":";
    }
}