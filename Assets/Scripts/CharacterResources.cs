using UnityEngine;

public abstract class CharacterResources : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private HealthBar _healthBar;

    private int _minHealth = 0;
    public int Health => _health;
    public int MaxHealth { get; protected set; } = 100;
    public int Damage { get; protected set; } = 25;
    public bool IsDead => _health == _minHealth;

    public virtual void TakeDamage(int damage)
    {   
        _health = Mathf.Max(_health - damage, _minHealth);
        _healthBar.UpdateUI();

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
        return true;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
