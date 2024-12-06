using UnityEngine;

public class EnemyResources : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private int _minHealth = 0;
    private int _damage = 25;

    private bool _isDead => _health == _minHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerResources>(out var player))
        {
            player.TakeDamage(_damage);
        }
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
