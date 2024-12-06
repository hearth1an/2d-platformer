using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private int _health;
        
    private int _minHealth = 0;

    private bool _isDead => _health == _minHealth;

    public int MaxHealth { get; private set; } = 100;
    public int Damage { get; private set; } = 25;
    public int Health => _health;

    private void Awake()
    {        
        _health = MaxHealth;       
    }

    public bool TryHeal(int value)
    {
        if (_health >= MaxHealth)
            return false;
        
        _health += value;
        if (_health > MaxHealth)
        {
            _health = MaxHealth;
        }
        return true;  
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Max(_health - damage, _minHealth);
        
        if (_isDead)
        {
            gameObject.SetActive(false);
        }
    }
}
