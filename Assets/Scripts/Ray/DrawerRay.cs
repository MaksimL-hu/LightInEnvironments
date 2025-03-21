using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerRay : MonoBehaviour
{
    [SerializeField] private float _drawingTime;
    [SerializeField] private PathBuilder _pathBuilder;
    [SerializeField] private Ray _prefab;
    [SerializeField] private Slider _timeSlider;

    [SerializeField] private List<Ray> _rays = new List<Ray>();

    private Coroutine _drawing;

    private void OnEnable()
    {
        _pathBuilder.CalculatedEnd += Draw;
        _timeSlider.onValueChanged.AddListener(ChangeDrawingTime);
    }

    private void OnDisable()
    {
        _pathBuilder.CalculatedEnd -= Draw;
        _timeSlider.onValueChanged.RemoveListener(ChangeDrawingTime);
    }

    private void Draw()
    {
        for (int i = 0; i < _rays.Count; i++)
            Destroy(_rays[i].gameObject);

        _rays.Clear();

        if (_drawing != null)
            StopCoroutine(_drawing);

        _drawing = StartCoroutine(DrawRays());
    }

    private IEnumerator DrawRays()
    {
        for (int i = 0; i < _pathBuilder.PointsCount - 1; i++)
        {
            Vector3 start = _pathBuilder.GetPoint(i);
            Vector3 end = _pathBuilder.GetPoint(i + 1);
            float time = 0;

            var ray = Instantiate(_prefab, transform);
            var stretcher = ray.GetComponent<StretcherBetweenPoints>();
            _rays.Add(ray);

            while (time < _drawingTime)
            {
                time += Time.deltaTime;
                stretcher.Stretch(start, Vector3.Lerp(start, end, time / _drawingTime));
                yield return null;
            }

            stretcher.Stretch(start, end);
        }
    }

    private void ChangeDrawingTime(float value)
    {
        _drawingTime = value;
    }
}