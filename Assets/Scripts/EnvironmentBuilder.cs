using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder : MonoBehaviour
{
    [SerializeField] private List<Environment> _environments;
    [SerializeField] private ContentForPanels _contentForPanels;
    [SerializeField] private Environment _prefab;

    private float _spacing;

    public int Count => _environments.Count;

    private void Start()
    {
        _spacing = _environments[0].gameObject.transform.localScale.y;
    }

    private void OnEnable()
    {
        _contentForPanels.CountPanelsChanged += PlaceEnvironments;
    }

    private void OnDisable()
    {
        _contentForPanels.CountPanelsChanged -= PlaceEnvironments;
    }

    private void PlaceEnvironments()
    {
        int countPanels = _contentForPanels.CountPanels;
        int currentCountPanels = _environments.Count;

        if (_environments.Count < countPanels)
        {
            for (int i = 0; i < countPanels - currentCountPanels; i++)
            {
                Environment environment = Instantiate(_prefab, transform);
                environment.gameObject.transform.position = new Vector3(0, _environments[_environments.Count - 1].gameObject.transform.position.y - _spacing, 0);
                _environments.Add(environment);
                environment.BuildEnvironment(_contentForPanels.GetPanel(_environments.Count - 1));
            }
        }
        else
        {
            for (int i = 0; i < currentCountPanels - countPanels; i++)
            {
                Environment environment = _environments[_environments.Count - 1];
                _environments.Remove(environment);
                Destroy(environment.gameObject);
            }
        }
    }

    public Environment GetEnvironment(int index)
    {
        return _environments[index];
    }
}