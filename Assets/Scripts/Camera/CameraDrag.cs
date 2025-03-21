using UnityEngine;

public abstract class CameraDrag : MonoBehaviour
{
    [SerializeField] protected Sensitivity Sensitivity;

    protected Vector3 DragOrigin;

    private void Update()
    {
        Drag();
    }

    protected abstract void Drag();
}