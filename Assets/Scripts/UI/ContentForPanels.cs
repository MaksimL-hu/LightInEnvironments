using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentForPanels : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private InputPanelForRefractiveIndex _panel;
    [SerializeField] private float _heightBetweenPanels;
    [SerializeField] private List<InputPanelForRefractiveIndex> _panelsForRefractiveIndex;

    public event Action CountPanelsChanged;

    private float _spacing;

    public int CountPanels => _panelsForRefractiveIndex.Count;

    private void Start()
    {
        _spacing = Mathf.Abs(_verticalLayoutGroup.spacing);
    }

    private void OnEnable()
    {
        _inputField.onEndEdit.AddListener(ChangeVewPort);
    }

    private void OnDisable()
    {
        _inputField.onEndEdit.RemoveListener(ChangeVewPort);
    }

    private void ChangeVewPort(string arg)
    {
        int countPanel;

        if (int.TryParse(arg, out countPanel) == false)
            return;

        if (countPanel < 2)
            return;

        float newHeight = ++countPanel * _heightBetweenPanels + _spacing;
        _transform.sizeDelta = new Vector2(_transform.sizeDelta.x, newHeight);

        int countExistingPanel = _panelsForRefractiveIndex.Count;

        if(countPanel > countExistingPanel) 
        {
            for (int i = 0; i < countPanel - countExistingPanel - 1; i++)
            {
                InputPanelForRefractiveIndex panel = Instantiate(_panel, _transform);
                panel.SetIndex(countExistingPanel + i + 1);
                _panelsForRefractiveIndex.Add(panel);
            }
        }
        else
        {
            for (int i = 0; i < countExistingPanel - countPanel + 1; i++)
            {
                InputPanelForRefractiveIndex panel = _panelsForRefractiveIndex[_panelsForRefractiveIndex.Count - 1];
                _panelsForRefractiveIndex.Remove(panel);
                Destroy(panel.gameObject);
            }
        }

        CountPanelsChanged?.Invoke();
    }

    public InputPanelForRefractiveIndex GetPanel(int index)
    {
        return _panelsForRefractiveIndex[index];
    }
}