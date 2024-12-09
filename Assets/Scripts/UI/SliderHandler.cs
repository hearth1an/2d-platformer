using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private CharacterResources _resources;
    private float _fillTime = 1f;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _resources.MaxHealth;
        _slider.minValue = _resources.MinHealth;
        _slider.value = _resources.Health;
    }

    public void ChangeValue(float value)
    {
        StartCoroutine(Fill(value));
    }

    private IEnumerator Fill(float value)
    {
        float estimatedTime = 0;

        while (estimatedTime < _fillTime)
        {
            estimatedTime += Time.deltaTime;
            _slider.value = Mathf.MoveTowards(_slider.value, value, estimatedTime / _fillTime);
            yield return null;
        }
    }

}
