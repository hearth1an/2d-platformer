using UnityEngine;

public abstract class CharacterResources : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private SliderHandler _sliderHanndler;
    
    public int Health => _health;
    public int MaxHealth { get; protected set; } = 100;
    public int Damage { get; protected set; } = 25;

    public int MinHealth { get; protected set; } = 0;
    public bool IsDead => _health == MinHealth;

    public virtual void TakeDamage(int damage)
    {   
        _health = Mathf.Max(_health - damage, MinHealth);
        _healthBar.UpdateUI();
        _sliderHanndler.ChangeValue(_health);

        if (IsDead)
        {
            Die();
        }
    }

    public virtual bool TryHeal(int value)
    {
        if (_health >= MaxHealth)
            return false;

        _health = Mathf.Min(_health + value, MaxHealth);
        _healthBar.UpdateUI();
        _sliderHanndler.ChangeValue(_health);
        return true;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
