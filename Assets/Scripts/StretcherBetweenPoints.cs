using UnityEngine;

public class StretcherBetweenPoints : MonoBehaviour
{
    public void Stretch(Vector3 _startPoint, Vector3 _endPoint)
    {
        float distance = Vector3.Distance(_startPoint, _endPoint);

        transform.localScale = new Vector3(transform.localScale.x, distance, transform.localScale.z);

        transform.position = (_startPoint + _endPoint) / 2;

        Vector2 direction = (_endPoint - _startPoint).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }
}