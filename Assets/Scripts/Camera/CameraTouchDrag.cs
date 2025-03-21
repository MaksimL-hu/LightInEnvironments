using UnityEngine;

public class CameraTouchDrag : CameraDrag
{ 
    protected override void Drag()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector3 difference = DragOrigin - touchPosition;

                    transform.position += difference * Sensitivity.SensitivityValue;
                    break;
            }
        }
    }
}