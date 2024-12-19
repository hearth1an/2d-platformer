using UnityEngine;

public class HealthUIHandler : MonoBehaviour
{
    [SerializeField] private HealthHandler _characterResources;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private SliderHandler _sliderHanndler;

    private void Awake()
    { 
        _characterResources.HealthChanged += ChangeValue;
    }

    private void OnDestroy()
    {
        _characterResources.HealthChanged -= ChangeValue;
    }

    private void ChangeValue(int value)
    {
        _healthBar.UpdateUI();
        _sliderHanndler.ChangeValue(value);
    }
}
