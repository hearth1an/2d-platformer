using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthHandler _characterResources;
    [SerializeField] private Text _text;

    public void UpdateUI()
    {
        _text.text = _characterResources.Health + "/" + _characterResources.MaxHealth;
    }
}
