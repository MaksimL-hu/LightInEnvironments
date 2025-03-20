using UnityEngine;

public abstract class CameraDrag : MonoBehaviour
{
    [SerializeField] protected float DragSpeed = 0.01f;

    protected Vector3 DragOrigin;

    private void Update()
    {
        Drag();
    }

    protected abstract void Drag();
}