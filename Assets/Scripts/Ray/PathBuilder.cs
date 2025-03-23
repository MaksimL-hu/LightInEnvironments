using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] private EnvironmentBuilder _environmentBuilder;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private TMP_InputField _inputStartAngle;

    [SerializeField] private List<Vector3> _points;

    private float _startingAngle;
    private Environment _currentEnvironment;
    private Environment _nextEnvironment;
    private TextMeshProUGUI _placeholderAngle;

    private float _infOffset = 1000f;
    private float _pathY;
    private float _angle;
    private float _offset;

    public event Action CalculatedEnd;

    public int PointsCount => _points.Count;

    private void Start()
    {
        _points = new List<Vector3>();
        _pathY = _environmentBuilder.GetEnvironment(0).gameObject.transform.localScale.y;
        _placeholderAngle = _inputStartAngle.placeholder.GetComponent<TextMeshProUGUI>();
        ChangeStartingAngle(_placeholderAngle.text);
    }

    private void OnEnable()
    {
        _inputStartAngle.onValueChanged.AddListener(ChangeStartingAngle);
    }

    private void OnDisable()
    {
        _inputStartAngle.onValueChanged.RemoveListener(ChangeStartingAngle);
    }

    private void ChangeStartingAngle(string value)
    {
        if (float.TryParse(value, out float startingAngle))
        {
            if (startingAngle < 0)
                _startingAngle = Mathf.Abs(startingAngle);
            else if (startingAngle > 90)
                _startingAngle = 90;
            else
                _startingAngle = startingAngle;

            _inputStartAngle.text = _startingAngle.ToString();
            _placeholderAngle.text = _startingAngle.ToString();
        }
    }

    private float GetOffset(float angle)
    {
        return _pathY * (Mathf.Tan(angle * Mathf.Deg2Rad));
    }

    private float GetCriticalAngle(float startingAngle)
    {
        return Mathf.Asin(_nextEnvironment.Refraction / _currentEnvironment.Refraction) * Mathf.Rad2Deg;
    }

    private float GetAngle(float startingAngle)
    {
        return Mathf.Asin(Mathf.Sin(startingAngle * Mathf.Deg2Rad) * _currentEnvironment.Refraction / _nextEnvironment.Refraction) * Mathf.Rad2Deg;
    }

    public void CalculatePath()
    {
        if (_environmentBuilder.Count < 2)
            return;

        _angle = _startingAngle;

        _points.Clear();
        _points.Add(_startPoint.position);

        if (_angle == 90f)
        {
            _points.Add(_points[_points.Count - 1] + new Vector3(_infOffset, 0, 0));
            CalculatedEnd?.Invoke();

            return;
        }

        _offset = GetOffset(_startingAngle);
        _points.Add(_points[0] + new Vector3(_offset, -_pathY, 0));

        _currentEnvironment = _environmentBuilder.GetEnvironment(0);
        _nextEnvironment = _environmentBuilder.GetEnvironment(1);

        int currentEnvironmentIndex = 1;
        int maxEnvironmentIndex = _environmentBuilder.Count - 1;
        bool isReflection = false;

        while (currentEnvironmentIndex >= 0 && currentEnvironmentIndex <= maxEnvironmentIndex)
        {
            float criticalAngle = GetCriticalAngle(_angle);

            if (Mathf.Abs(criticalAngle - _angle) < 0.01f)
            {
                _points.Add(_points[_points.Count - 1] + new Vector3(_infOffset, 0, 0));
                CalculatedEnd?.Invoke();

                return;
            }
            else if (_angle > criticalAngle && isReflection == false)
            {
                _angle = 180 - _angle;
                _offset = GetOffset(_angle);

                _points.Add(_points[_points.Count - 1] + new Vector3(-_offset, _pathY, 0));

                isReflection = !isReflection;
                currentEnvironmentIndex--;

                if (currentEnvironmentIndex <= 0)
                    break;

                _currentEnvironment = _environmentBuilder.GetEnvironment(currentEnvironmentIndex);
                _nextEnvironment = _environmentBuilder.GetEnvironment(currentEnvironmentIndex - 1);

                currentEnvironmentIndex--;
            }
            else
            {
                _angle = GetAngle(_angle);
                _offset = GetOffset(_angle);

                if (isReflection)
                {
                    _points.Add(_points[_points.Count - 1] + new Vector3(_offset, _pathY, 0));
                    currentEnvironmentIndex--;

                    if (currentEnvironmentIndex < 0)
                        break;

                    _currentEnvironment = _nextEnvironment;
                    _nextEnvironment = _environmentBuilder.GetEnvironment(currentEnvironmentIndex);
                }
                else
                {
                    _points.Add(_points[_points.Count - 1] + new Vector3(_offset, -_pathY, 0));
                    currentEnvironmentIndex++;

                    if (currentEnvironmentIndex > maxEnvironmentIndex)
                        break;

                    _currentEnvironment = _nextEnvironment;
                    _nextEnvironment = _environmentBuilder.GetEnvironment(currentEnvironmentIndex);
                }
            }

        }

        CalculatedEnd?.Invoke();
    }

    public Vector3 GetPoint(int index)
    {
        return _points[index];
    }
}