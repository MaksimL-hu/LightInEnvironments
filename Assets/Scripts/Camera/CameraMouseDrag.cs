using UnityEngine;

public class CameraMouseDrag : CameraDrag
{
    protected override void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = DragOrigin - mousePosition;

            transform.position += difference * DragSpeed;
        }
    }
}