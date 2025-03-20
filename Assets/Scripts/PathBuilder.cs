using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] private StretcherBetweenPoints _ray;
    [SerializeField] private EnvironmentBuilder _environmentBuilder;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private List<Vector3> _points;
    [SerializeField] private float _startingAngle;
    [SerializeField] private float _drawingTime;
 
    [SerializeField] private List<StretcherBetweenPoints> _rays;
    private Environment _currentEnvironment;
    private Environment _nextEnvironment;
    private Coroutine _drawing;

    private float _pathY;
    private float _angle;
    private float _offset;

    private void Start()
    {
        _points = new List<Vector3>();
        _pathY = _environmentBuilder.GetEnvironment(0).gameObject.transform.localScale.y;
        _rays = new List<StretcherBetweenPoints>();
    }

    private void BuildRay(Vector3 startPosition, Vector3 endPosition)
    {
        var ray = Instantiate(_ray, transform);
        ray.Stretch(startPosition, endPosition);
        _rays.Add(ray);
    }

    private float GetOffset(float angle)
    {
        return _pathY * (Mathf.Tan(angle * Mathf.Deg2Rad));
    }

    private float GetAngle(float startingAngle)
    {
        return Mathf.Asin(Mathf.Sin(startingAngle * Mathf.Deg2Rad) * _currentEnvironment.Refraction / _nextEnvironment.Refraction) * Mathf.Rad2Deg;
    }

    public void CalculatePath()
    {
        if (_environmentBuilder.Count < 2)
            return;

        if(_drawing != null)
            StopCoroutine(_drawing);

        for (int i = 0; i < _rays.Count; i++)
        {
            Destroy(_rays[i].gameObject);
        }

        _angle = _startingAngle;

        _rays.Clear();

        _points.Clear();
        _points.Add(_startPoint.position);

        _offset = GetOffset(_startingAngle);
        _points.Add(_points[0] + new Vector3(_offset, -_pathY, 0));

        _currentEnvironment = _environmentBuilder.GetEnvironment(0);
        _nextEnvironment = _environmentBuilder.GetEnvironment(1);

        for (int i = 1; i < _environmentBuilder.Count - 1; i++)
        {
            _angle = GetAngle(_angle);
            _offset = GetOffset(_angle);
            _currentEnvironment = _nextEnvironment;
            _nextEnvironment = _environmentBuilder.GetEnvironment(i + 1);

            _points.Add(_points[_points.Count - 1] + new Vector3(_offset, -_pathY, 0));
        }

        _angle = GetAngle(_angle);
        _offset = GetOffset(_angle);
        _points.Add(_points[_points.Count - 1] + new Vector3(_offset, -_pathY, 0));

        _drawing = StartCoroutine(DrawRays());
    }

    private IEnumerator DrawRays()
    {
        for (int i = 0; i < _points.Count - 1; i++)
        {
            Vector3 start = _points[i];
            Vector3 end = _points[i + 1];
            float time = 0;

            var ray = Instantiate(_ray, transform);
            _rays.Add(ray);

            while (time < _drawingTime)
            {
                time += Time.deltaTime;
                ray.Stretch(start, Vector3.Lerp(start, end, time / _drawingTime));
                yield return null;
            }

            ray.Stretch(start, end);
        }
    }
}