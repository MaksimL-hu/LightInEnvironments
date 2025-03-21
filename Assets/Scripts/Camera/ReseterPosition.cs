using UnityEngine;
using UnityEngine.UI;

public class ReseterPosition : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ResetPosition);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ResetPosition);
    }

    private void ResetPosition()
    {
        transform.position = _startPosition;
    }
}