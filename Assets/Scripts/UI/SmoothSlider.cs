using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeSmooth;

    [ContextMenu(nameof(ChangeValue))]
    public void ChangeValue()
    {
        StartCoroutine(ChangingValue());
    }

    private IEnumerator ChangingValue()
    {
        float start = _slider.minValue;
        float end = _slider.maxValue;

        float time = 0f;

        while (time < _timeSmooth)
        {
            time += Time.deltaTime;

            _slider.value = Mathf.Lerp(start, end, time / _timeSmooth);

            yield return null;
        }

        _slider.value = end;
    }
}
