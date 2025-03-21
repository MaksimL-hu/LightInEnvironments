using UnityEngine;
using UnityEngine.UI;

public abstract class SettingsButton : MonoBehaviour
{
    [SerializeField] protected Button Button;
    [SerializeField] protected GameObject Panel;
    [SerializeField] protected Camera Camera;

    protected CameraMouseDrag MouseDrag;
    protected CameraTouchDrag TouchDrag;

    private void Start()
    {
        Camera.TryGetComponent<CameraMouseDrag>(out MouseDrag);
        Camera.TryGetComponent<CameraTouchDrag>(out TouchDrag);
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(ChangeMenuState);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(ChangeMenuState);
    }

    protected abstract void ChangeMenuState();
}