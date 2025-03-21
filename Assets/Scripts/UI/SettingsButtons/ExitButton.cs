using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}